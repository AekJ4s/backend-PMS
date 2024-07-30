using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesQuarterlyScore
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesActivityId { get; set; }

    public virtual CbesActivity? CbesActivity { get; set; }

    public virtual ICollection<CbesPointOfQuarter> CbesPointOfQuarters { get; set; } = new List<CbesPointOfQuarter>();
}
