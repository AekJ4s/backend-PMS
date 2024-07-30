using CBEsApi.Data;
using CBEsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBEsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CBEsPermissionController : ControllerBase
    {
        private CbesManagementContext _db = new CbesManagementContext();

        [HttpGet(Name = "GetAllPermissions")]
        public ActionResult<Response> GetAllPermissions()
        {
            List<CbesPermission> permissions = CbesPermission.GetAll(_db);
            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = permissions
            });
        }

        [HttpGet("{id}", Name = "GetPermissionById")]
        public ActionResult<Response> GetPermissionById(int id)
        {
            CbesPermission permissions = CbesPermission.GetById(_db, id);
            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = permissions
            });
        }

    }
}