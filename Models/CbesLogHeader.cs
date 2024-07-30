using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesLogHeader
{
    public int Id { get; set; }

    public int? Round { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesLogTypeId { get; set; }

    public int? CbesLogId { get; set; }

    public int? CbesId { get; set; }

    public int? UpdateBy { get; set; }

    public virtual Cbe? Cbes { get; set; }

    public virtual CbesLog? CbesLog { get; set; }

    public virtual CbesLogType? CbesLogType { get; set; }

    public virtual ICollection<CbesWithSubSupervisorLog> CbesWithSubSupervisorLogs { get; set; } = new List<CbesWithSubSupervisorLog>();

    public virtual ICollection<CbesWithSupervisorLog> CbesWithSupervisorLogs { get; set; } = new List<CbesWithSupervisorLog>();

    public virtual ICollection<MaturityWithSupervisorLog> MaturityWithSupervisorLogs { get; set; } = new List<MaturityWithSupervisorLog>();

    public virtual CbesUser? UpdateByNavigation { get; set; }
}
