using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsPositionDto;
using CBEsApi.Dtos.CBEsUserDto;

namespace CBEsApi.Models
{
    public class CbesPositionMetadata
    {

    }

    [MetadataType(typeof(CbesPositionMetadata))]
    public partial class CbesPosition
    {
        public static List<CBEsPositionDto> GetAll(CbesManagementContext db)
        {
            List<CBEsPositionDto>? positions = db.CbesPositions.Where(q => q.IsDeleted != true)
                                                               .Include(u => u.CbesUsers)
                                                                .Select(s => new CBEsPositionDto
                                                                {
                                                                    Id = s.Id,
                                                                    Name = s.Name,
                                                                    CreateDate = s.CreateDate,
                                                                    UpdateDate = s.UpdateDate,
                                                                    IsDeleted = s.IsDeleted,
                                                                    IsLastDelete = s.IsLastDelete,
                                                                    CreateBy = s.CreateBy,
                                                                    UpdateBy = s.UpdateBy,
                                                                    CbesUsers = s.CbesUsers.Select(u => new CbesUserDto
                                                                    {
                                                                        // ใส่ข้อมูลเพิ่มเติมตามที่คุณต้องการจาก CbesUserDto
                                                                        Id = u.Id,
                                                                        Username = u.Username,
                                                                        Fullname = u.Fullname,
                                                                        IsDeleted = u.IsDeleted,
                                                                        PositionId = u.PositionId,
                                                                    }).ToList()
                                                                }).ToList();
            return positions;
        }

        public static CBEsPositionDto GetById(CbesManagementContext db, int id)
        {
            var position = db.CbesPositions
                .Where(q => q.Id == id && q.IsDeleted != true)
                .Include(u => u.CbesUsers)
                .Select(s => new CBEsPositionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CreateDate = s.CreateDate,
                    UpdateDate = s.UpdateDate,
                    IsDeleted = s.IsDeleted,
                    IsLastDelete = s.IsLastDelete,
                    CreateBy = s.CreateBy,
                    UpdateBy = s.UpdateBy,
                    CbesUsers = s.CbesUsers.Select(u => new CbesUserDto
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Fullname = u.Fullname,
                        IsDeleted = u.IsDeleted,
                        PositionId = u.PositionId,
                        // ใส่ข้อมูลเพิ่มเติมตามที่คุณต้องการจาก CbesUserDto
                    }).ToList()
                })
                .FirstOrDefault();

            return position ?? new CBEsPositionDto();
        }

        internal Dtos.CBEsDto.CBEsPositionDto Select(Func<object, Dtos.CBEsDto.CBEsPositionDto> value)
        {
            throw new NotImplementedException();
        }
    }
}