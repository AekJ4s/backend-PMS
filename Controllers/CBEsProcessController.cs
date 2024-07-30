

// using CBEsApi.Data;
// using CBEsApi.Models;
// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Mvc;

// namespace CBEsApi.Controllers
// {
//     [Route("api/[controller]")]
//     public class CBEsProcessController : Controller
//     {
//         private readonly ILogger<CBEsProcessController> _logger;

//         public CBEsProcessController(ILogger<CBEsProcessController> logger)
//         {
//             _logger = logger;
//         }

//         private CbesManagementContext _db = new CbesManagementContext();

//         /// <summary>
//         /// GetInverseProcessInside
//         /// </summary>
//         [HttpGet(Name = "GetInverseProcessInside")]
//         public ActionResult<Response> GetInverseProcessInside(int id)
//         {
//             if (id != null)
//             {
//                 CbesProcess process = _db.CbesProcesses.FirstOrDefault(x => x.Id == id);
//                 Icol<CbesProcess> processes = CbesProcess.GetInverseProcessinside(_db, process);

//                 return Ok(new Response
//                 {
//                     Status = 200,
//                     Message = "Success",
//                     Data = processes
//                 });
//             }
//             else
//             {
//                 return Ok(new Response
//                 {
//                     Status = 400,
//                     Message = "Not Found",
//                     Data = null
//                 });
//             }


//         }
//     }
// }