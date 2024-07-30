using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesMaturityLog
{
    public int Id { get; set; }

    public string? Detail { get; set; }

    public int? Lv { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesProcessLogId { get; set; }

    public virtual CbesProcessLog? CbesProcessLog { get; set; }

    public virtual ICollection<MaturityWithSupervisorLog> MaturityWithSupervisorLogs { get; set; } = new List<MaturityWithSupervisorLog>();
}
