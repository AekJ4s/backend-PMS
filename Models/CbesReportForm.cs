using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesReportForm
{
    public int Id { get; set; }

    public DateTime? SubmitDate { get; set; }

    public string? Remark { get; set; }

    public bool? IsAccept { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UserVerify { get; set; }

    public int? CbesPlanningId { get; set; }

    public virtual CbesPlanning? CbesPlanning { get; set; }

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual CbesUser? UserVerifyNavigation { get; set; }
}
