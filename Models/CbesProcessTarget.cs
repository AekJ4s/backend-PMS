using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessTarget
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public int? TargetPoint { get; set; }

    public int? CbesProcessId { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }

    public virtual ICollection<CbesProcessResult> CbesProcessResults { get; set; } = new List<CbesProcessResult>();
}
