using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CBEsApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CBEsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly ILogger<AuthorizationController> _logger;

    public AuthorizationController(ILogger<AuthorizationController> logger)
    {
        _logger = logger;
    }

    private const string TokenSecret = "YourSecretKeyForAuthenticationOfApplicationDeveloper";

    private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(60);

    private CbesManagementContext _db = new CbesManagementContext();

    public struct RequestCreate
    {
        /// <summary>
        /// Username of the User
        /// </summary>
        /// <example>admin</example>
        /// <required>true</required>
        public string? Username { get; set; }

        /// <summary>
        /// Password of the User
        /// </summary>
        /// <example>1234</example>
        /// <required>true</required>
        public string? Password { get; set; }
    }

    public struct RegisterCreate
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    /// <summary>
    /// Generate Token
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /GenerateToken
    ///     
    ///     {
    ///         "username": "string"
    ///     }
    ///     
    /// </remarks>
    [HttpPost("/GenerateToken", Name = "GenerateToken")]
    public string GenerateToken([FromBody] CbesUser user)
    {
        // สร้าง instance ของ JwtSecurityTokenHandler ซึ่งเป็นคลาสที่ใช้ในการสร้างและจัดการ JWT
        var tokenHandler = new JwtSecurityTokenHandler();

        // สร้าง key สำหรับการลงลายเซ็นต์ของ token โดยการเข้ารหัส string เป็น bytes
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        // สร้างรายการของ Claims ซึ่งจะถูกบรรจุใน token
        var claims = new List<Claim>
    {
        // Claim ที่เก็บชื่อผู้ใช้
        new Claim(ClaimTypes.Name, user.Username),
        
        // Claim ที่เก็บ ID ของผู้ใช้
        new Claim("ID", user.Id.ToString()),
    };

        // สร้าง SecurityTokenDescriptor ซึ่งใช้ในการกำหนดคุณสมบัติของ token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // กำหนด ClaimsIdentity ซึ่งประกอบด้วย claims ที่เราสร้างขึ้น
            Subject = new ClaimsIdentity(claims),

            // กำหนดเวลาหมดอายุของ token
            Expires = DateTime.UtcNow.Add(TokenLifetime),

            // กำหนด issuer (ผู้สร้าง token)
            Issuer = "http://localhost:8000",

            // กำหนด audience (กลุ่มเป้าหมายของ token)
            Audience = "http://localhost:8000",

            // กำหนด SigningCredentials ซึ่งประกอบด้วย key และอัลกอริธึมที่ใช้ในการลงลายเซ็นต์ token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // สร้าง token โดยใช้ tokenDescriptor ที่กำหนด
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // ตรวจสอบว่า token ถูกสร้างขึ้นหรือไม่
        if (token != null)
        {
            // แปลง token เป็น string และส่งกลับ
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
        else
        {
            // หากไม่สามารถสร้าง token ได้ ให้คืนค่าข้อความแสดงความล้มเหลว
            return "Failed to write token.";
        }
    }


    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/Authorization/login
    ///     
    ///     {
    ///         "username": "admin",
    ///         "password": "1234"
    ///     }
    ///     
    /// </remarks>
    [HttpPost("Login", Name = "Login")]
    public ActionResult Login([FromBody] RequestCreate request)
    {
        CbesUser user = _db.CbesUsers.FirstOrDefault(doc => doc.Username == request.Username && doc.IsDeleted == false);

        if (user == null)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = "Bad Request to Username",
                Data = null
            });
        }

        // Check if user is locked out due to consecutive failed login attempts
        if (user.IsLog == false && user.LoginFailedCount >= 5)
        {
            // If the user is still within the lockout period
            if (user.LoginDate != null && user.LoginDate.Value.AddMinutes(5) > DateTime.UtcNow)
            {
                TimeSpan timeToWait = user.LoginDate.Value.AddMinutes(5) - DateTime.UtcNow;
                int secondsToWait = (int)Math.Ceiling(timeToWait.TotalSeconds);

                return StatusCode(429, new Response
                {
                    Status = 429,
                    Message = $"ผู้ใช้ถูก Block เนื่องจากกรอกข้อมูลผิดต่อเนื่อง {user.LoginFailedCount}/5 ครั้ง โปรดลองอีกครั้งในภายหลัง (รออีก {secondsToWait} วินาที)",
                    Data = null
                });
            }

            // If the lockout period has passed, reset failed attempts and unlock the account
            if (user.LoginDate.Value.AddMinutes(5) <= DateTime.UtcNow)
            {
                user.IsLog = true;
                user.LoginFailedCount = 0;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        // If password is incorrect, increment login failed count and save changes
        if (user.Password != request.Password)
        {
            user.LoginFailedCount = user.LoginFailedCount + 1;
            user.LoginDate = DateTime.UtcNow;

            // Check if login failed count exceeds limit
            if (user.LoginFailedCount >= 5)
            {
                user.IsLog = false;
            }

            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();

            return StatusCode(401, new Response
            {
                Status = 401,
                Message = "Unauthorized to Password",
                Data = null
            });
        }

        // Successful login
        if (user.IsLog == true && user.Password == request.Password)
        {
            string bearerToken = GenerateToken(user);
            user.LoginFailedCount = 0;

            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(new Response
            {
                Status = 200,
                Message = "Login Success",
                Data = new
                {
                    token = bearerToken,
                }
            });
        }

        return StatusCode(401, new Response
        {
            Status = 401,
            Message = "Unauthorized to Password",
            Data = null
        });
    }


    [HttpPost("Register", Name = "Register")]
    public ActionResult<Response> Register(RegisterCreate registerCreate)
    {
        CbesUser user = new CbesUser
        {
            Username = registerCreate.Username,
            Password = registerCreate.Password,
        };


        try
        {
            user = CbesUser.Create(_db, user);
        }
        catch
        {
            // Handle unique key constraint violation (duplicate username)
            return StatusCode(409, new Response
            {
                Status = 409,
                Message = "Username already exists",
                Data = null
            });
        }

        return Ok(new Response
        {
            Status = 201,
            Message = "Created Successfully",
            Data = user
        });
    }


}
