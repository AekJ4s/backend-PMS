using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessPlanningLog
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesPlannnigId { get; set; }

    public int? CbesProcessLogId { get; set; }

    public virtual ICollection<CbesIndicatorLog> CbesIndicatorLogs { get; set; } = new List<CbesIndicatorLog>();

    public virtual CbesPlanning? CbesPlannnig { get; set; }

    public virtual CbesPlanningLog? CbesProcessLog { get; set; }
}
