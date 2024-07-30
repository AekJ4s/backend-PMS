using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesPointOfQuarterLog
{
    public int Id { get; set; }

    public int? TargetPoint { get; set; }

    public int? ResultPoint { get; set; }

    public int? Quarter { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesQuarterlyScoreLogId { get; set; }

    public virtual CbesQuarterlyScoreLog? CbesQuarterlyScoreLog { get; set; }
}
