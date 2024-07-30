using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using CBEsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBEsApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CBEsPlaningController : ControllerBase
    {
        private CbesManagementContext _db = new CbesManagementContext();

        // Endpoint GET /api/CBEsPlaining/{id}
        // [HttpGet("{id}", Name = "GetAllPlainingOfCBEs")]
        // public ActionResult GetAllPlanningByCBEsId(int id)
        // {

        //     // Implementation here
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }else{
        //     List<CBEsPlanningListDto> cbes = CbesPlanning.GetAllPlanningByCBEsId(_db,id);
        //     }
        //     return Ok($"data of Planning {id}");
        // }

        // Endpoint GET /api/CBEsPlaining/{id}
        [HttpGet("{id}", Name = "GetPlanningById")]
        public ActionResult GetPlanningById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"data of Planning {id}");
        }

        // Endpoint POST /api/CBEsPlaining
        [HttpPost(Name = "PostPlaining")]
        public ActionResult PostPlaining(CbesProcess cbesProcess)
        {
            // Implementation here
            return Ok(cbesProcess);
        }

        // Endpoint PUT /api/CBEsPlaining
        [HttpPut(Name = "PutPlaining")]
        public ActionResult PutPlaining(CbesProcess cbesProcess)
        {
            // Implementation here
            return Ok(cbesProcess);
        }

        // Endpoint PUT /api/CBEsPlaining/target
        [HttpPut("target", Name = "PutPlainingTarget")]
        public ActionResult PutPlainingTarget(CbesProcessPlanning cbesProcessTarget)
        {
            // Implementation here
            return Ok(cbesProcessTarget);
        }

        // Endpoint PUT /api/CBEsPlaining/result
        [HttpPut("result", Name = "PutPlainingResult")]
        public ActionResult PutPlainingResult(CbesProcessTarget cbesProcessTarget)
        {
            // Implementation here
            return Ok(cbesProcessTarget);
        }

        // Endpoint POST /api/CBEsPlaining/send/{id}
        [HttpPost("send/{id}", Name = "PostPlainingSend")]
        public ActionResult PostPlainingSend(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Send plaining for id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/history
        [HttpGet("history", Name = "GetAllPlainingHistory")]
        public ActionResult GetAllPlainingHistory()
        {
            // Implementation here
            return Ok("All plaining history");
        }

        // Endpoint GET /api/CBEsPlaining/history/{id}
        [HttpGet("history/{id}", Name = "GetPlainingHistory")]
        public ActionResult GetPlainingHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"History for plaining id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/history/target/{id}
        [HttpGet("history/target/{id}", Name = "GetPlainingHistoryTarget")]
        public ActionResult GetPlainingHistoryTarget(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"History target for plaining id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/history/result/{id}
        [HttpGet("history/result/{id}", Name = "GetPlainingHistoryResult")]
        public ActionResult GetPlainingHistoryResult(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"History result for plaining id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/bin
        [HttpGet("bin", Name = "GetPlainingBin")]
        public ActionResult GetPlainingBin()
        {
            // Implementation here
            return Ok("Plaining bin data");
        }

        // Endpoint PUT /api/CBEsPlaining/bin/CancelDelete/{id}
        [HttpPut("bin/CancelDelete/{id}", Name = "PutPlainingBinCancelDelete")]
        public ActionResult PutPlainingBinCancelDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Cancel delete for plaining bin id {id}");
        }

        // Endpoint PUT /api/CBEsPlaining/bin/LastDelete/{id}
        [HttpPut("bin/LastDelete/{id}", Name = "PutPlainingBinLastDelete")]
        public ActionResult PutPlainingBinLastDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Last delete for plaining bin id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/bin/{id}
        [HttpGet("bin/{id}", Name = "GetPlainingBinById")]
        public ActionResult GetPlainingBinById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Plaining bin data for id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/report/{id}
        [HttpGet("report/{id}", Name = "GetPlainingReportById")]
        public ActionResult GetPlainingReportById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Plaining report data for id {id}");
        }

        // Endpoint POST /api/CBEsPlaining/report/CreateDraft/{id}
        [HttpPost("report/CreateDraft/{id}", Name = "PostPlainingReportCreateDraft")]
        public ActionResult PostPlainingReportCreateDraft(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Create draft report for id {id}");
        }

        // Endpoint POST /api/CBEsPlaining/report/send/{id}
        [HttpPost("report/send/{id}", Name = "PostPlainingReportSend")]
        public ActionResult PostPlainingReportSend(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok($"Send report for id {id}");
        }

        // Endpoint GET /api/CBEsPlaining/ExportToPdf
        [HttpGet("ExportToPdf", Name = "GetPlainingExportToPdf")]
        public ActionResult GetPlainingExportToPdf()
        {
            // Implementation here
            return Ok("Export all plaining to PDF");
        }

        // Endpoint GET /api/CBEsPlaining/report/ExportToPdf
        [HttpGet("report/ExportToPdf", Name = "GetPlainingReportExportToPdf")]
        public ActionResult GetPlainingReportExportToPdf()
        {
            // Implementation here
            return Ok("Export report to PDF");
        }
    }
}