using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsLogDto
    {
    public int Id { get; set; }

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public string? Detail { get; set; }

    public int? Year { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLastDelete { get; set; }

    public int CbesLogHeaderId { get; set; }

    public virtual ICollection<CBEsProcessLogDto>? CbesProcessLogs { get; set; }

    }
}