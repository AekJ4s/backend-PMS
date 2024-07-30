using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class Cbe
{
    public int Id { get; set; }

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLastDelete { get; set; }

    public int? CreateBy { get; set; }

    public virtual ICollection<CbesLogHeader> CbesLogHeaders { get; set; } = new List<CbesLogHeader>();

    public virtual ICollection<CbesPlanningLog> CbesPlanningLogs { get; set; } = new List<CbesPlanningLog>();

    public virtual ICollection<CbesPlanning> CbesPlannings { get; set; } = new List<CbesPlanning>();

    public virtual ICollection<CbesProcess> CbesProcesses { get; set; } = new List<CbesProcess>();

    public virtual ICollection<CbesTargetResultLogHeader> CbesTargetResultLogHeaders { get; set; } = new List<CbesTargetResultLogHeader>();

    public virtual ICollection<CbesWithSubSupervisor> CbesWithSubSupervisors { get; set; } = new List<CbesWithSubSupervisor>();

    public virtual ICollection<CbeswithSupervisor> CbeswithSupervisors { get; set; } = new List<CbeswithSupervisor>();

    public virtual CbesUser? CreateByNavigation { get; set; }
}
