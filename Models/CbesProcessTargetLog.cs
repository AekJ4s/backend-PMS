using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessTargetLog
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public int? TargetPoint { get; set; }

    public int? UpdateBy { get; set; }

    public int? CbesProcessLogId { get; set; }

    public int? CbesTargetResultLogHeaderId { get; set; }

    public virtual CbesProcessLog? CbesProcessLog { get; set; }

    public virtual ICollection<CbesProcessResultLog> CbesProcessResultLogs { get; set; } = new List<CbesProcessResultLog>();

    public virtual CbesTargetResultLogHeader? CbesTargetResultLogHeader { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
