using System.Data.Common;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using CBEsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBEsApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CBEsLogHeaderController : ControllerBase
    {
        private CbesManagementContext _db = new CbesManagementContext();
      

   

    }
}
