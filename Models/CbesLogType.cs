using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesLogType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CbesLogHeader> CbesLogHeaders { get; set; } = new List<CbesLogHeader>();
}
