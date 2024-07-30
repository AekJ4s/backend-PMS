using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesPointOfQuarter
{
    public int Id { get; set; }

    public int? TargetPoint { get; set; }

    public int? ResultPoint { get; set; }

    public int? Quarter { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesQuarterlyScoreId { get; set; }

    public virtual ICollection<CbesComment> CbesComments { get; set; } = new List<CbesComment>();

    public virtual CbesQuarterlyScore? CbesQuarterlyScore { get; set; }
}
