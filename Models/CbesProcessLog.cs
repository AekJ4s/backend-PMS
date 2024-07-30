using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessLog
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Weight { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ProcessLogHeaderId { get; set; }

    public int? CbesLogId { get; set; }

    public virtual CbesLog? CbesLog { get; set; }

    public virtual ICollection<CbesMaturityLog> CbesMaturityLogs { get; set; } = new List<CbesMaturityLog>();

    public virtual ICollection<CbesProcessResultLog> CbesProcessResultLogs { get; set; } = new List<CbesProcessResultLog>();

    public virtual ICollection<CbesProcessTargetLog> CbesProcessTargetLogs { get; set; } = new List<CbesProcessTargetLog>();

    public virtual ICollection<CbesProcessLog> InverseProcessLogHeader { get; set; } = new List<CbesProcessLog>();

    public virtual CbesProcessLog? ProcessLogHeader { get; set; }
}
