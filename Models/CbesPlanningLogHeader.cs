using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesPlanningLogHeader
{
    public int Id { get; set; }

    public int? Round { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesPlanningLogId { get; set; }

    public int? CbesPlanningLogTypeId { get; set; }

    public virtual CbesPlanningLog? CbesPlanningLog { get; set; }

    public virtual CbesPlanningLogType? CbesPlanningLogType { get; set; }

    public virtual ICollection<CbesPlanningLog> CbesPlanningLogs { get; set; } = new List<CbesPlanningLog>();
}
