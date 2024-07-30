using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesTargetResultLogHeader
{
    public int Id { get; set; }

    public int? Round { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesTargetResultLogTypeId { get; set; }

    public int? CbesId { get; set; }

    public int? UpdateBy { get; set; }

    public int? CbesLogId { get; set; }

    public virtual Cbe? Cbes { get; set; }

    public virtual CbesLog? CbesLog { get; set; }

    public virtual ICollection<CbesProcessResultLog> CbesProcessResultLogs { get; set; } = new List<CbesProcessResultLog>();

    public virtual ICollection<CbesProcessTargetLog> CbesProcessTargetLogs { get; set; } = new List<CbesProcessTargetLog>();

    public virtual ICollection<CbesReportFormLog> CbesReportFormLogs { get; set; } = new List<CbesReportFormLog>();

    public virtual CbesTragetResultLogType? CbesTargetResultLogType { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
