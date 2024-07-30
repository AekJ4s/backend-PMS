using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessResultLog
{
    public int Id { get; set; }

    public int? ResultPoint { get; set; }

    public string? Remark { get; set; }

    public int? UpdateBy { get; set; }

    public int? CbesProcessLogId { get; set; }

    public int? CbesProcessTargetLogId { get; set; }

    public int? CbesTargetResultLogHeaderId { get; set; }

    public virtual CbesProcessLog? CbesProcessLog { get; set; }

    public virtual CbesProcessTargetLog? CbesProcessTargetLog { get; set; }

    public virtual CbesTargetResultLogHeader? CbesTargetResultLogHeader { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
