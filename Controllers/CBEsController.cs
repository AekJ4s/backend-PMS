using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using CBEsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBEsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CBEsController : ControllerBase


    {
        private CbesManagementContext _db = new CbesManagementContext();

        /// <summary>
        /// Get All CBEs ⚡️
        /// </summary>
        [HttpGet(Name = "GetCBEs")]
        public ActionResult GetCBEs()
        {
            List<CBEsDto> cbes = Cbe.GetAll(_db);
            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = cbes
            });
        }

        /// <summary>
        /// ค้นหา CBEs ตาม id ที่ส่งเข้ามาโดยจะมี LogHeader ติดไปด้วย ⚡️
        /// </summary>
        [HttpGet("{id}", Name = "GetByIdCbe")]
        public ActionResult<Response> CbesWithLogHeader(int id)
        {

            var userClaimsString = User.FindFirst("ID")?.Value;
            int userClaims = Convert.ToInt32(userClaimsString); // Get Update By Or Create By Token

            CBEsDto CbesWithLogHeader = CbesLogHeader.GetCBEs(_db, id);

            return Ok(new Response
            {
                Status = 200,
                Message = "Success To Get CBEs and CBEsLog !!! ",
                Data = CbesWithLogHeader
            });
        }

        /// <summary>
        /// Update CBEsMaturity ⚡️หน้าบ้านหลัง Get ข้อมูลไปจะมีตัวเดียว ส่งตัวเดิมที่ Remark และ Round(หากเปลี่ยนรอบ) ที่เปลี่ยนกลับมา
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /CBEs
        /// 
        /// CBE data:{
        /// CBELogData:[{}]
        /// CBEProcessDATA:[{}]
        /// }
        /// </remarks>
        [HttpPut("UpdateCBE", Name = "UpdateCBE")]
        public ActionResult<Response> UpdateCBE(Cbe cbe)
        {
            // try
            // {

                var userClaimsString = User.FindFirst("ID")?.Value;
                int userClaims = Convert.ToInt32(userClaimsString); // Get Update By Or Create By Token
                if (userClaims != 0)
                {
                    Cbe cbes = Cbe.Update(_db, cbe, userClaims);
                    CbesLog cbesLog = CbesLog.Copy(_db, cbe, userClaims);
                    CbesLogHeader cbesLogHeader = CbesLogHeader.CreateUpdateLog(_db, cbes, cbesLog, userClaims);
                    return Ok(new Response
                    {
                        Status = 200,
                        Message = "Success To Update CBEs and createNew Log",
                        Data = cbes
                    });
                }
                else
                {
                    return Unauthorized(new Response
                    {
                        Status = 401,
                        Message = "Please Login",
                        Data = null
                    });
                }
            // }
            // catch
            // {
            //     return BadRequest(new Response
            //     {
            //         Status = 400,
            //         Message = "BadRequest",
            //         Data = null
            //     });
            // }
        }

        /// <summary>
        /// Update CBEsMaturity ⚡️หน้าบ้านหลัง Get ข้อมูลไปจะมีตัวเดียว ส่งตัวเดิมที่ Remark และ Round(หากเปลี่ยนรอบ) ที่เปลี่ยนกลับมา
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /CBEs
        /// 
        /// CBE data:{
        /// CBELogData:[{}]
        /// CBEProcessDATA:[{}]
        /// }
        /// </remarks>
        [HttpPut("UpdateMaturity", Name = "UpdateMaturity")]
        public ActionResult<Response> UpdateMaturity(Cbe cbe)
        {

            var userClaimsString = User.FindFirst("ID")?.Value;
            int userClaims = Convert.ToInt32(userClaimsString); // Get Update By Or Create By Token
            if (userClaims != 0)
            {
                Cbe cbes = Cbe.Update(_db, cbe, userClaims);
                CbesLog cbesLog = CbesLog.Copy(_db, cbe, userClaims);
                CbesLogHeader cbesLogHeader = CbesLogHeader.CreateUpdateSupervisorLog(_db, cbes, cbesLog, userClaims);
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success To Update CBEs and CBEsLog !!! ",
                    Data = cbes
                });
            }
            else
            {
                return Unauthorized(new Response
                {
                    Status = 401,
                    Message = "Please Login",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Update Supervisors And SubSupervisor ⚡️หน้าบ้านหลัง Get ข้อมูลไปและกดเพิ่มผู้รับผิดชอบเองและส่งข้อมูลกลับมาหลังบ้าน
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /CBEs
        /// 
        /// CBE data:{
        /// CBELogData:[{}]
        /// CbeswithSupervisors:[{}]
        /// CbesWithSubSupervisors:[{}]
        /// CbesMaturities: [],
        /// MaturityWithSupervisorLogs: [],
        /// 
        /// }
        /// </remarks>
        [HttpPut("UpdateSupervisor", Name = "UpdateSupervisor")]
        public ActionResult<Response> UpdateSupervisor(Cbe cbe)
        {
            var userClaimsString = User.FindFirst("ID")?.Value;
            int userClaims = Convert.ToInt32(userClaimsString); // Get Update By Or Create By Token
            if (userClaims != 0)
            {
                Cbe cbes = Cbe.Update(_db, cbe, userClaims);
                CbesLog cbesLog = CbesLog.Copy(_db, cbe, userClaims);
                CbesLogHeader cbesLogHeader = CbesLogHeader.CreateUpdateLog(_db, cbes, cbesLog, userClaims);
                _db.SaveChanges();
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success To Update CBEs and CBEsLog !!! ",
                    Data = cbesLogHeader
                });
            }
            else
            {
                return Unauthorized(new Response
                {
                    Status = 401,
                    Message = "Please Login",
                    Data = null
                });
            }
        }


        // [HttpGet("Process/{id}", Name = "Get")] 
        // public ActionResult GetProcessId(int id)
        // {
        //     CbesProcess process = CbesProcess.GetById(id,_db);

        //     return Ok(new Response
        //     {
        //         Status = 200,
        //         Message = "Success Get By Process Id",
        //         Data = process
        //     });
        // }

        /// <summary>
        /// Get Planning By CBE ID ⚡️
        /// </summary>
        [HttpGet("GetAllPlanningByCBEs/{id}", Name = "GetAllPlanningByCBEs")]
        public ActionResult GetAllPlanningByCBEsId(int id)
        {
            CBEsWithPlanningDto cbeswithplanning = Cbe.GetAllPlanningOfCBEs(_db, id);
            return Ok(new Response
            {
                Status = 200,
                Message = "Success To Get All Planning By CBEs Id",
                Data = cbeswithplanning
            });
        }


        /// <summary>
        /// สร้าง CBEs ขึ้นมาที่จะมี Process หรือไม่มีก็ได้ แต่บังคับให้มี CbesLogHeaders ที่มี Round กับ Remark มาด้วย ⚡️
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /CBEs
        ///     
        ///         {
        ///         "ThaiName": "Test Post CBE With Header",
        ///         "EngName": "English Heaader",
        ///         "ShortName": "ECHEAS",
        ///         "CbesLogHeaders":[
        ///             {
        ///                 "Round" : 1,
        ///                 "Remark" : "This is Log Remark"
        ///             }
        ///         ],
        ///         "CbesProcesses": [
        ///             {
        ///                 "Name": "Process1",
        ///                 "Weight": 50,
        ///                 "InverseProcessHeader": [
        ///                     {
        ///                         "Name": "Process1.1",
        ///                         "Weight": 25
        ///                     },
        ///                     {
        ///                         "Name": "Process1.2",
        ///                         "Weight": 25,
        ///                         "InverseProcessHeader": [
        ///                             {
        ///                                 "Name": "Process1.2.1",
        ///                                 "Weight": 25,
        ///                                 "InverseProcessHeader": []
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             },
        ///             {
        ///                 "Name": "Process2",
        ///                 "Weight": 50,
        ///                 "InverseProcessHeader": [
        ///                     {
        ///                         "Name": "Process2.1",
        ///                         "Weight": 25,
        ///                         "InverseProcessHeader": [
        ///                             {
        ///                                 "Name": "Process2.2.1",
        ///                                 "Weight": 25,
        ///                                 "InverseProcessHeader": []
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        /// }
        /// </remarks>
        [HttpPost(Name = "Create")]
        public ActionResult<Response> CreateCBEs(Cbe dataFromFrontEnd, CbesManagementContext db)
        {
            try
            {
                var userClaimsString = User.FindFirst("ID")?.Value;
                int userClaims = Convert.ToInt32(userClaimsString); // Get Update By Or Create By Token
                if (userClaims == 0)
                {
                    return Unauthorized(new Response
                    {
                        Status = 401,
                        Message = "Login Before Create " + dataFromFrontEnd.ThaiName,
                        Data = null
                    });
                }
                else
                {
                    // ไปสร้าง CBEs ตัวจริงก่อน
                    Cbe cbeOriginal = Cbe.Create(_db, dataFromFrontEnd, userClaims);
                    CbesLog cbeLogs = CbesLog.Create(_db, dataFromFrontEnd, userClaims);
                    CbesLogHeader cbeHeader = CbesLogHeader.Create(_db, dataFromFrontEnd, cbeOriginal, cbeLogs, userClaims);
                    cbeOriginal.CbesLogHeaders.First().CbesLog = null;
                    return Ok(new Response
                    {
                        Status = 200,
                        Message = "Success To Create " + dataFromFrontEnd.ThaiName,
                        Data = cbeOriginal
                    });
                }
            }
            catch
            {
                return BadRequest(new Response
                {
                    Status = 400,
                    Message = "Fail To Create CBEs and CBEsLog",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get CBE History ⚡️
        /// </summary>
        [HttpGet("history/{id}", Name = "GetByIdHistory")]
        public ActionResult<Response> GetByIdHistory(int id)
        {

            List<CbesLogHeaderDto> cbeHistory = CbesLogHeader.GetAll(_db, id);
            try
            {
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success to Get history",
                    Data = cbeHistory
                });
            }
            catch
            {
                return BadRequest(new Response
                {
                    Status = 400,
                    Message = "Fail to Get history",
                    Data = null
                });
            }

        }

        /// <summary>
        /// Get All Bin ⚡️
        /// </summary>
        [HttpGet("bin", Name = "GetAllCBEsBin")]
        public ActionResult<Response> GetAllCBEsBin()
        {
            List<CBEsDto> cbes = Cbe.GetAllBin(_db);

            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = cbes
            });
        }

        /// <summary>
        /// Cancel Delete Bin By ID ⚡️
        /// </summary>
        [HttpPut("bin/CancelDelete/{id}", Name = "UpdateDeleteCBE")]
        public ActionResult UpdateCancelDeleteCBE(int id)
        {
            try
            {
                CBEsDto cbes = Cbe.CancelDelete(_db, id);

                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success",
                    Data = cbes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    Status = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Delete CBE By ID ⚡️
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteCBE")]
        public ActionResult<Response> DeleteCBE(int id)
        {
            try
            {
                CBEsDto cbeDto = Cbe.Delete(_db, id);
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Cbe Deleted",
                    Data = cbeDto
                });
            }
            catch
            {
                // ถ้าไม่พบข้อมูล Cbe ตาม id ที่ระบุ
                return NotFound(new Response
                {
                    Status = 404,
                    Message = "Cbe not found",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Last Delete Bin By ID ⚡️
        /// </summary>
        [HttpDelete("bin/LastDelete/{id}", Name = "UpdateLastDeleteCBE")]
        public ActionResult UpdateLastDeleteCBE(int id)
        {
            try
            {
                CBEsDto cbes = Cbe.LastDelete(_db, id);
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Success",
                    Data = cbes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    Status = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
