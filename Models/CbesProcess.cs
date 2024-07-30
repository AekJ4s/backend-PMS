using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcess
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Weight { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ProcessHeaderId { get; set; }

    public int? CbesId { get; set; }

    public virtual Cbe? Cbes { get; set; }

    public virtual ICollection<CbesIndicator> CbesIndicators { get; set; } = new List<CbesIndicator>();

    public virtual ICollection<CbesMaturity> CbesMaturities { get; set; } = new List<CbesMaturity>();

    public virtual ICollection<CbesProcessPlanning> CbesProcessPlannings { get; set; } = new List<CbesProcessPlanning>();

    public virtual ICollection<CbesProcessResult> CbesProcessResults { get; set; } = new List<CbesProcessResult>();

    public virtual ICollection<CbesProcessTarget> CbesProcessTargets { get; set; } = new List<CbesProcessTarget>();

    public virtual ICollection<CbesProcess> InverseProcessHeader { get; set; } = new List<CbesProcess>();

    public virtual CbesProcess? ProcessHeader { get; set; }
}
