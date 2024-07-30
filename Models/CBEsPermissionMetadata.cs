using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;

namespace CBEsApi.Models
{
    public class CbesPermissionMetadata
    {

    }

    [MetadataType(typeof(CbesPermissionMetadata))]
    public partial class CbesPermission
    {
        public static CbesPermission Create(CbesManagementContext db, CbesPermission permission)
        {
            permission.CreateDate = DateTime.Now;
            permission.UpdateDate = DateTime.Now;
            permission.IsDeleted = false;
            db.CbesPermissions.Add(permission);
            db.SaveChanges();

            return permission;
        }
        public static List<CbesPermission> GetAll(CbesManagementContext db)
        {
            List<CbesPermission> permissions = db.CbesPermissions.Where(q => q.IsDeleted != true).ToList();
            return permissions;
        }

        public static CbesPermission GetById(CbesManagementContext db, int id)
        {
            CbesPermission? permissions = db.CbesPermissions.Where(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            return permissions ?? new CbesPermission();
        }

    }
}