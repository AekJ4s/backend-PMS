using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesPlanningLog
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Year { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesId { get; set; }

    public int? CbesPlanningLogHeaderId { get; set; }

    public int? UpdateBy { get; set; }

    public virtual Cbe? Cbes { get; set; }

    public virtual ICollection<CbesActivityLog> CbesActivityLogs { get; set; } = new List<CbesActivityLog>();

    public virtual ICollection<CbesIndicatorLog> CbesIndicatorLogs { get; set; } = new List<CbesIndicatorLog>();

    public virtual CbesPlanningLogHeader? CbesPlanningLogHeader { get; set; }

    public virtual ICollection<CbesPlanningLogHeader> CbesPlanningLogHeaders { get; set; } = new List<CbesPlanningLogHeader>();

    public virtual ICollection<CbesProcessPlanningLog> CbesProcessPlanningLogs { get; set; } = new List<CbesProcessPlanningLog>();

    public virtual ICollection<CbesReportFormLog> CbesReportFormLogs { get; set; } = new List<CbesReportFormLog>();

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
