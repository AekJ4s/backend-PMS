using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CBEsApi.Data;

namespace CBEsApi.Models
{
    public class CbesRoleWithPermissionMetadata
    {

    }

    [MetadataType(typeof(CbesRoleWithPermissionMetadata))]
    public partial class CbesRoleWithPermission
    {
        public static CbesRoleWithPermission Create(CbesManagementContext db, CbesRoleWithPermission cbesRolePermission)
        {
            cbesRolePermission.CreateDate = DateTime.Now;
            cbesRolePermission.UpdateDate = DateTime.Now;
            cbesRolePermission.IsDeleted = false;
            db.CbesRoleWithPermissions.Add(cbesRolePermission);
            db.SaveChanges();

            return cbesRolePermission;
        }
    }

}