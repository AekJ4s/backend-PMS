using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesIndicator
{
    public int Id { get; set; }

    public bool? IsChecked1 { get; set; }

    public bool? IsChecked2 { get; set; }

    public bool? IsChecked3 { get; set; }

    public bool? IsChecked4 { get; set; }

    public bool? IsChecked5 { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesActivityId { get; set; }

    public int? CbesProcessId { get; set; }

    public int? CbesPlanningId { get; set; }

    public virtual CbesActivity? CbesActivity { get; set; }

    public virtual CbesPlanning? CbesPlanning { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }
}
