using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesUser
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? LoginDate { get; set; }

    public int? LoginFailedCount { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLog { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? PositionId { get; set; }

    public int? UserWithRoleId { get; set; }

    public virtual ICollection<Cbe> Cbes { get; set; } = new List<Cbe>();

    public virtual ICollection<CbesLogHeader> CbesLogHeaders { get; set; } = new List<CbesLogHeader>();

    public virtual ICollection<CbesLog> CbesLogs { get; set; } = new List<CbesLog>();

    public virtual ICollection<CbesPermission> CbesPermissionCreateByNavigations { get; set; } = new List<CbesPermission>();

    public virtual ICollection<CbesPermission> CbesPermissionUpdateByNavigations { get; set; } = new List<CbesPermission>();

    public virtual ICollection<CbesPlanningLog> CbesPlanningLogs { get; set; } = new List<CbesPlanningLog>();

    public virtual ICollection<CbesPlanning> CbesPlannings { get; set; } = new List<CbesPlanning>();

    public virtual ICollection<CbesPosition> CbesPositionCreateByNavigations { get; set; } = new List<CbesPosition>();

    public virtual ICollection<CbesPosition> CbesPositionUpdateByNavigations { get; set; } = new List<CbesPosition>();

    public virtual ICollection<CbesProcessResultLog> CbesProcessResultLogs { get; set; } = new List<CbesProcessResultLog>();

    public virtual ICollection<CbesProcessTargetLog> CbesProcessTargetLogs { get; set; } = new List<CbesProcessTargetLog>();

    public virtual ICollection<CbesReportForm> CbesReportFormCreateByNavigations { get; set; } = new List<CbesReportForm>();

    public virtual ICollection<CbesReportFormLog> CbesReportFormLogUpdateByNavigations { get; set; } = new List<CbesReportFormLog>();

    public virtual ICollection<CbesReportFormLog> CbesReportFormLogUserVerifyNavigations { get; set; } = new List<CbesReportFormLog>();

    public virtual ICollection<CbesReportForm> CbesReportFormUserVerifyNavigations { get; set; } = new List<CbesReportForm>();

    public virtual ICollection<CbesRole> CbesRoleCreateByNavigations { get; set; } = new List<CbesRole>();

    public virtual ICollection<CbesRole> CbesRoleUpdateByNavigations { get; set; } = new List<CbesRole>();

    public virtual ICollection<CbesTargetResultLogHeader> CbesTargetResultLogHeaders { get; set; } = new List<CbesTargetResultLogHeader>();

    public virtual ICollection<CbesUserWithRole> CbesUserWithRoleCreateByNavigations { get; set; } = new List<CbesUserWithRole>();

    public virtual ICollection<CbesUserWithRole> CbesUserWithRoleUpdateByNavigations { get; set; } = new List<CbesUserWithRole>();

    public virtual ICollection<CbesUserWithRole> CbesUserWithRoleUsers { get; set; } = new List<CbesUserWithRole>();

    public virtual ICollection<CbesWithSubSupervisor> CbesWithSubSupervisorCreateByNavigations { get; set; } = new List<CbesWithSubSupervisor>();

    public virtual ICollection<CbesWithSubSupervisor> CbesWithSubSupervisorUpdateByNavigations { get; set; } = new List<CbesWithSubSupervisor>();

    public virtual ICollection<CbeswithSupervisor> CbeswithSupervisorCreateByNavigations { get; set; } = new List<CbeswithSupervisor>();

    public virtual ICollection<CbeswithSupervisor> CbeswithSupervisorUpdateByNavigations { get; set; } = new List<CbeswithSupervisor>();

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual ICollection<CbesUser> InverseCreateByNavigation { get; set; } = new List<CbesUser>();

    public virtual ICollection<CbesUser> InverseUpdateByNavigation { get; set; } = new List<CbesUser>();

    public virtual ICollection<MaturityWithSupervisor> MaturityWithSupervisorCreateByNavigations { get; set; } = new List<MaturityWithSupervisor>();

    public virtual ICollection<MaturityWithSupervisor> MaturityWithSupervisorUpdateByNavigations { get; set; } = new List<MaturityWithSupervisor>();

    public virtual CbesPosition? Position { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
