using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesUserWithRole
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? RoleId { get; set; }

    public int? UserId { get; set; }

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual CbesRole? Role { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }

    public virtual CbesUser? User { get; set; }
}
