using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsWithPlanningDto
    {
    public int Id { get; set; }

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public virtual List<CBEsPlanningDto> CbesPlannings { get; set; } 

    }
}