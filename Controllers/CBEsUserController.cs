using CBEsApi.Data;
using CBEsApi.Dtos.CBEsUserDto;
using CBEsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CBEsApi.Controllers
{
    [Route("api/[controller]")]
    public class CBEsUserController : Controller
    {
        private readonly ILogger<CBEsUserController> _logger;

        public CBEsUserController(ILogger<CBEsUserController> logger)
        {
            _logger = logger;
        }

        private CbesManagementContext _db = new CbesManagementContext();

        /// <summary>
        /// Get All Users
        /// </summary>
        [HttpGet(Name = "GetUsers")]
        public ActionResult<Response> GetUsers()
        {
            List<CbesUserDto> users = CbesUser.GetAll(_db);

            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = users
            });
        }
    }
}