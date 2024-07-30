using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
    public class CbesUserWithRoleMetadata
    {
        // Define metadata properties if needed
    }

    [MetadataType(typeof(CbesUserWithRoleMetadata))]
    public partial class CbesUserWithRole
    {
        public static List<CbesUser> GetAll(CbesManagementContext db)
        {
            List<CbesUser> roleUsers = db.CbesUsers.Where(q => q.IsDeleted != true).ToList();
            return roleUsers;
        }

        public static CbesUserWithRole GetById(CbesManagementContext db, int id)
        {
            CbesUserWithRole? roleUser = db.CbesUserWithRoles
                .Where(q => q.Id == id && q.IsDeleted != true)
                .Include(p => p.User)
                .FirstOrDefault();
            return roleUser ?? new CbesUserWithRole();
        }

    }
}
