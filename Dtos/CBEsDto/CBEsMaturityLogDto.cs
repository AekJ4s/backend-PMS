using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsMaturityLogDto
    {
   public int Id { get; set; }

    public string? Detail { get; set; }

    public int? Lv { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesProcessLogId { get; set; }

    public virtual ICollection<CBEsMaturitySupervisorLogsDto> MaturityWithSupervisorLogs { get; set; }

    }
}