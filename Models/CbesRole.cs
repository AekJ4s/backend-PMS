using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLastDelete { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<CbesRoleWithPermission> CbesRoleWithPermissions { get; set; } = new List<CbesRoleWithPermission>();

    public virtual ICollection<CbesUserWithRole> CbesUserWithRoles { get; set; } = new List<CbesUserWithRole>();

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
