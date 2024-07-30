using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesMaturity
{
    public int Id { get; set; }

    public string? Detail { get; set; }

    public int? Lv { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesProcessId { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }

    public virtual ICollection<MaturityWithSupervisor> MaturityWithSupervisors { get; set; } = new List<MaturityWithSupervisor>();
}
