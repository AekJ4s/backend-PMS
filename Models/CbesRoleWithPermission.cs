using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesRoleWithPermission
{
    public int Id { get; set; }

    public bool? IsChecked { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? RoleId { get; set; }

    public int? PermissionId { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public virtual CbesPermission? Permission { get; set; }

    public virtual CbesRole? Role { get; set; }
}
