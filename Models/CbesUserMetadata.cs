using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsUserDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
    public class CbesUserMetadata { }

    [MetadataType(typeof(CbesUserMetadata))]
    public partial class CbesUser
    {
        // Create Action
        public static CbesUser Create(CbesManagementContext db, CbesUser user)
        {
            user.CreateDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            user.IsDeleted = false;
            db.CbesUsers.Add(user);
            db.SaveChanges();

            return user;
        }

        //Get All Action
        public static List<CbesUserDto> GetAll(CbesManagementContext db)
        {
            List<CbesUserDto> users = db.CbesUsers
                                        .Where(q => q.IsDeleted != true)
                                        .Select(u => new CbesUserDto
                                        {
                                            Id = u.Id,
                                            Username = u.Username,
                                            Fullname = u.Fullname,
                                            IsDeleted = u.IsDeleted,
                                            PositionId = u.PositionId,
                                        })
                                        .ToList();
            return users;
        }
        //Get ID Action
        public static CbesUser GetById(CbesManagementContext db, int? id)
        {
            CbesUser? user = db.CbesUsers.Where(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            return user ?? new CbesUser();
        }

        public static CbesUserDto UserDtoGetById(CbesManagementContext db, int? id)
        {
            CbesUser? user = db.CbesUsers.Where(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            if (user != null)
            {
                return new CbesUserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Fullname = user.Fullname,
                    IsDeleted = user.IsDeleted,
                    PositionId = user.PositionId
                };
            }
            else
            {
                return new CbesUserDto();
            }
        }

        //Update Action
        public static CbesUser Update(CbesManagementContext db, CbesUser user)
        {
            user.UpdateDate = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return user;
        }

        //Delete Action
        public static CbesUser Delete(CbesManagementContext db, int id)
        {
            CbesUser user = GetById(db, id);
            user.IsDeleted = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return user;
        }


    }

}