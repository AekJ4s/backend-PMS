using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesIndicatorLog
{
    public int Id { get; set; }

    public bool? IsChecked1 { get; set; }

    public bool? IsChecked2 { get; set; }

    public bool? IsChecked3 { get; set; }

    public bool? IsChecked4 { get; set; }

    public bool? IsChecked5 { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesActivityLogId { get; set; }

    public int? CbesProcessPlanningLogId { get; set; }

    public int? CbesPlannnigLogId { get; set; }

    public virtual CbesActivityLog? CbesActivityLog { get; set; }

    public virtual CbesPlanningLog? CbesPlannnigLog { get; set; }

    public virtual CbesProcessPlanningLog? CbesProcessPlanningLog { get; set; }
}
