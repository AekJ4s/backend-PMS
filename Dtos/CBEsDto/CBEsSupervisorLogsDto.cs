using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsWithSupervisorLogsDto
    {
    public int Id { get; set; }

    public int? CbesId { get; set; }

    public int? PositionId { get; set; }

    public virtual CBEsPositionDto? Position { get; set; }

    }
}