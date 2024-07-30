using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesActivityLog
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? HeaderId { get; set; }

    public int? CbesPlanningLogId { get; set; }

    public virtual ICollection<CbesIndicatorLog> CbesIndicatorLogs { get; set; } = new List<CbesIndicatorLog>();

    public virtual CbesPlanningLog? CbesPlanningLog { get; set; }

    public virtual ICollection<CbesQuarterlyScoreLog> CbesQuarterlyScoreLogs { get; set; } = new List<CbesQuarterlyScoreLog>();

    public virtual CbesActivityLog? Header { get; set; }

    public virtual ICollection<CbesActivityLog> InverseHeader { get; set; } = new List<CbesActivityLog>();
}
