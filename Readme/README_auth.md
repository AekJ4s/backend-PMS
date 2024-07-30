# Swagger & Jwt

```cs
builder.Services.AddSwaggerGen(options =>
{
    //TODO: Swagger Header
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API", Version = "v1", Description = "ASP.NET Core Web API" });

    // using System.Reflection; //TODO: XML Comment
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

    //TODO: Add Security Definition - Jwt (1)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });

    //TODO: Add Security Requirement by OpenApi - Jwt (2)
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

//TODO: Add Authorization -Jwt (3)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

app.UseAuthentication();
app.UseAuthorization();
```

## Generate Token Controller

```cs
using Microsoft.AspNetCore.Mvc;
using activityCore.Data;
using activityCore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace activityCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private const string TokenSecret = "YourSecretKeyForAuthenticationOfApplicationDeveloper";

        private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(10);

        private ActivityContext _db = new ActivityContext();

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public struct RegisterCreate
        {
            /// <summary>
            /// Username of User
            /// </summary>
            /// <example>John</example>
            /// <required>true</required>
            [Required(ErrorMessage = "User Name is required")]
            [RegularExpression(@"^[a-zA-Z0-9]*$")]
            public string? Username { get; set; }

            /// <summary>
            /// Password of User
            /// </summary>
            /// <example>1234</example>
            /// <required>true</required>
            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }

            /// <summary>
            /// Role of User
            /// </summary>
            /// <example>user</example>
            /// <required>true</required>
            public string? Role { get; set; }
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /GenerateToken
        ///     {
        ///         "id": 0,
        ///         "username": "string",
        ///         "password": "string",
        ///         "role": "admin",
        ///         "createDate": "2024-05-27T15:02:54.076Z",
        ///         "updateDate": "2024-05-27T15:02:54.076Z",
        ///         "isDelete": true
        ///     }
        ///
        /// </remarks>
        [HttpPost("token", Name = "GenerateToken")]
        public string GenerateToken([FromBody] User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role) //TODO: Add Role parameter
            // Add more claims as needed
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "http://localhost:8000",
                Audience = "http://localhost:8000",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            if (token != null)
            {
                var jwt = tokenHandler.WriteToken(token);
                return jwt;
            }
            else
            {
                return "Failed to write token.";
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Account/login
        ///     {
        ///         "username": "admin",
        ///         "password": "1234"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="409">Conflict: Username already exists</response>
        [HttpPost("login", Name = "Login")]
        public IActionResult Login([FromBody] User request)
        {
            User? user = _db.Users.FirstOrDefault(doc => doc.Username == request.Username && doc.Password == request.Password && doc.IsDelete == false);

            if (user == null)
            {
                return NotFound();
            }

            string bearerToken;

            try
            {
                bearerToken = GenerateToken(user);
            }
            catch
            {
                return BadRequest(new Response
                {
                    Code = 400,
                    Message = "Bad Request to Username & Password",
                    Data = null
                }
                );
            }
            return Ok(new Response
            {
                Code = 200,
                Message = "Login Success",
                Data = new
                {
                    token = bearerToken,
                    role = user.Role
                }
            });
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Account/register
        ///     {
        ///         "username": "john",
        ///         "password": "1234"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Created Successfully</response>
        /// <response code="409">Conflict: Username already exists</response>
        [HttpPost("register", Name = "Register")]
        public ActionResult Register(RegisterCreate registerCreate)
        {
            if (registerCreate.Role.IsNullOrEmpty())
            {
                registerCreate.Role = "user";
            }

            User user = new User
            {
                Username = registerCreate.Username,
                Password = registerCreate.Password,
                Role = registerCreate.Role,
            };

            try
            {
                user = Models.User.Create(_db, user);
            }
            catch
            {
                // Handle unique key constraint violation (duplicate username)
                return StatusCode(409, new Response
                {
                    Code = 409,
                    Message = "Username already exists",
                    Data = null
                });
            }

            return user.Username == null || user.Password == null || user.Role == null
                ? BadRequest(new Response
                {
                    Code = 400,
                    Message = "Bad Request",
                    Data = null
                })
                : Ok(new Response
                {
                    Code = 201,
                    Message = "Created Successfully",
                    Data = user
                });
        }
        
    }
}
```
