using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesPlanningLogType
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CbesPlanningLogHeader> CbesPlanningLogHeaders { get; set; } = new List<CbesPlanningLogHeader>();
}
