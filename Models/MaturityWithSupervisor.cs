using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class MaturityWithSupervisor
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? MaturityId { get; set; }

    public int? PositionId { get; set; }

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual CbesMaturity? Maturity { get; set; }

    public virtual CbesPosition? Position { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
