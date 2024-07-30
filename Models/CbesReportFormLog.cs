using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesReportFormLog
{
    public int Id { get; set; }

    public DateTime? SubmitDate { get; set; }

    public string? Remark { get; set; }

    public bool? IsAccept { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? UpdateBy { get; set; }

    public int? UserVerify { get; set; }

    public int? CbesPlanningLogId { get; set; }

    public int? CbesPlanningLogHeaderId { get; set; }

    public virtual CbesPlanningLog? CbesPlanningLog { get; set; }

    public virtual CbesTargetResultLogHeader? CbesPlanningLogHeader { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }

    public virtual CbesUser? UserVerifyNavigation { get; set; }
}
