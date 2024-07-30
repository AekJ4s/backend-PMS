using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesQuarterlyScoreLog
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesActivityLogId { get; set; }

    public virtual CbesActivityLog? CbesActivityLog { get; set; }

    public virtual ICollection<CbesPointOfQuarterLog> CbesPointOfQuarterLogs { get; set; } = new List<CbesPointOfQuarterLog>();
}
