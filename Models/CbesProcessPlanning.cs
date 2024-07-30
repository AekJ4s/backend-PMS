using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesProcessPlanning
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesProcessId { get; set; }

    public int? CbesPlanningId { get; set; }

    public virtual CbesPlanning? CbesPlanning { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }
}
