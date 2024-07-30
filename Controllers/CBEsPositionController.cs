using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CBEsApi.Models;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsPositionDto;

namespace CBEsApi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class CBEsPositionController : ControllerBase
    {
        private CbesManagementContext _db = new CbesManagementContext();

        [HttpGet(Name = "GetAllPositions")]
        public ActionResult GetAllPositions()
        {
            List<CBEsPositionDto> positions = CbesPosition.GetAll(_db);

            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = positions
            });
        }

        [HttpGet("{id}", Name = "GetPositionById")]
        public ActionResult GetPositionById(int id)
        {
            CBEsPositionDto position = CbesPosition.GetById(_db, id);

            if (position == null || position.Id == 0)
            {
                return NotFound(new Response
                {
                    Status = 404,
                    Message = "Position not found",
                    Data = null
                });
            }

            return Ok(new Response
            {
                Status = 200,
                Message = "Success",
                Data = position
            });

        }

    }
}