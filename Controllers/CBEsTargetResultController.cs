using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using CBEsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CBEsTargetResultController : ControllerBase
    {

        private readonly ILogger<CBEsTargetResultController> _logger;

        public CBEsTargetResultController(ILogger<CBEsTargetResultController> logger)
        {
            _logger = logger;
        }

        private CbesManagementContext _db = new CbesManagementContext();


        // Endpoint GET /api/CBEsTargetResult
        [HttpGet(Name = "GetAllTargetResult")]
        public ActionResult GetAllTargetResult(CbesProcess cbeProcess)
        {
            return Ok(cbeProcess);
        }

        // Endpoint GET /api/GetProcessWithTarget/{id}
        /// <summary>
        /// จะรับค่า CBEsId และ ค่า Year จาก Dropdown เพื่อมาหา Process ทั้งหมดและนำ Year เพื่อมาอ้างอิงถึงอีกสองปีที่จะคำนวณข้างหน้า
        /// </summary>
        [HttpGet("{id},{Year}", Name = "GetProcessWithTarget")]
        public ActionResult GetProcessWithTarget(int id, int Year,CbesManagementContext db)
        {
                CBEsDto? cbes = Cbe.GetById(db,id);
            if (cbes != null)
            {
                
                CbesProcess.GetProcessWithTarget(Year,cbes, _db);

                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success To Get Target",
                    Data = cbes,
                });
            }
            return BadRequest(new Response
                {
                    Status = 400,
                    Message = "Bad Request",
                    Data = null
                });
        }

        // Endpoint POST /api/CBEsTargetResult
        [HttpPost("target", Name = "PostTarget")]
        public ActionResult PostTarget(Cbe cbe,CbesManagementContext db)
        {
            var userClaimsString = User.FindFirst("ID")?.Value;
            int userClaims = Convert.ToInt32(userClaimsString); // Get Update By Or Create By Token
            
            if (cbe != null )
            {
            CbesProcess.CreateOrUpdate(cbe.CbesProcesses,db);
            
            string remark = cbe.CbesLogHeaders.FirstOrDefault().Remark;
            int? round = cbe.CbesLogHeaders.FirstOrDefault().Round;
            cbe.CbesLogHeaders = null;
            
            db.Cbes.Update(cbe);
            db.SaveChanges();
            CbesLog cbesLog = CbesLog.Copy(_db, cbe,userClaims);
            CbesTargetResultLogHeader cbesLogHeader = CbesTargetResultLogHeader.Create(cbe,remark,round,cbesLog,userClaims,db);
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success To POST Target",
                    Data = cbesLogHeader
                });
            }
            return BadRequest(new Response
                {
                    Status = 400,
                    Message = "Bad Request",
                    Data = null
                });
           
        }

        // Endpoint POST /api/CBEsTargetResult
        [HttpPost("result", Name = "PostResult")]
        public ActionResult PostResult(int? id, CbesProcessTarget cbesProcessTarget)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(cbesProcessTarget);
        }

        // Endpoint GET /api/CBEsTargetResult/history/{id}
        [HttpGet("history")]
        public ActionResult<string> GetAllHistory(Cbe cbe)
        {
            if (cbe == null)
            {
                return NotFound();
            }
            return Ok($"History for result {cbe}");
        }

        // Endpoint GET /api/CBEsTargetResult/history/{id}
        [HttpGet("history/{id}")]
        public ActionResult<string> GetHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"History for result {id}");
        }
    }

}