using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsProcessLogDto
    {
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Weight { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ProcessLogHeaderId { get; set; }

    public int? CbesLogId { get; set; }

    public virtual ICollection<CBEsMaturityLogDto>? CbesMaturityLogs { get; set; } 

    public virtual ICollection<CBEsProcessLogDto>? InverseProcessLogHeader { get; set; } 

    }
}