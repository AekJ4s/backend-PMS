using System;
using System.Collections.Generic;

namespace CBEsApi.Models;

public partial class CbesComment
{
    public int Id { get; set; }

    public string? Summarize { get; set; }

    public string? Problem { get; set; }

    public string? HowTo { get; set; }

    public int? CbesPointOfQuarterId { get; set; }

    public virtual CbesPointOfQuarter? CbesPointOfQuarter { get; set; }
}
