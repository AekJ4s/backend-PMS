using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsPlanningDto
    {
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Year { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsLastDelete { get; set; }
    public int? CbesId { get; set; }

    }
}