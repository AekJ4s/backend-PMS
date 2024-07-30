using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsMaturitySupervisorLogsDto
    {
    public int Id { get; set; }
    public bool? IsDeleted { get; set; }
    public int? MaturityLogId { get; set; }
    public int? CbesLogHeaderId { get; set; }
    public int? PositionId { get; set; }
    public virtual CBEsPositionDto? Position { get; set; }
    }
}