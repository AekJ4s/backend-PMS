using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesPlanning
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Year { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLastDelete { get; set; }

    public int? CreateBy { get; set; }

    public int? CbesId { get; set; }

    public virtual Cbe? Cbes { get; set; }

    public virtual ICollection<CbesActivity> CbesActivities { get; set; } = new List<CbesActivity>();

    public virtual ICollection<CbesIndicator> CbesIndicators { get; set; } = new List<CbesIndicator>();

    public virtual ICollection<CbesProcessPlanningLog> CbesProcessPlanningLogs { get; set; } = new List<CbesProcessPlanningLog>();

    public virtual ICollection<CbesProcessPlanning> CbesProcessPlannings { get; set; } = new List<CbesProcessPlanning>();

    public virtual ICollection<CbesReportForm> CbesReportForms { get; set; } = new List<CbesReportForm>();

    public virtual CbesUser? CreateByNavigation { get; set; }
}
