using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesActivity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? HeaderId { get; set; }

    public int? CbesPlanningId { get; set; }

    public virtual ICollection<CbesIndicator> CbesIndicators { get; set; } = new List<CbesIndicator>();

    public virtual CbesPlanning? CbesPlanning { get; set; }

    public virtual ICollection<CbesQuarterlyScore> CbesQuarterlyScores { get; set; } = new List<CbesQuarterlyScore>();

    public virtual CbesActivity? Header { get; set; }

    public virtual ICollection<CbesActivity> InverseHeader { get; set; } = new List<CbesActivity>();
}
