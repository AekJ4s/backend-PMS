using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessResult
{
    public int Id { get; set; }

    public int? ResultPoint { get; set; }

    public string? Remark { get; set; }

    public int? CbesProcessId { get; set; }

    public int? CbesProcessTargetId { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }

    public virtual CbesProcessTarget? CbesProcessTarget { get; set; }
}
