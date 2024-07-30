using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesLog
{
    public int Id { get; set; }

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public int? Year { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLastDelete { get; set; }

    public int? UpdateBy { get; set; }

    public virtual CbesLogHeader? CbesLogHeader { get; set; }

    public virtual ICollection<CbesProcessLog> CbesProcessLogs { get; set; } = new List<CbesProcessLog>();

    public virtual ICollection<CbesTargetResultLogHeader> CbesTargetResultLogHeaders { get; set; } = new List<CbesTargetResultLogHeader>();

    public virtual ICollection<CbesWithSubSupervisorLog> CbesWithSubSupervisorLogs { get; set; } = new List<CbesWithSubSupervisorLog>();

    public virtual ICollection<CbesWithSupervisorLog> CbesWithSupervisorLogs { get; set; } = new List<CbesWithSupervisorLog>();

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
