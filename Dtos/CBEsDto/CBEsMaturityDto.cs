using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsMaturityDto
    {
    public int Id { get; set; }

    public string? Detail { get; set; }
    
    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? Lv { get; set; }

    public int? CbesProcessId { get; set; }

    public virtual ICollection<CBEsMaturitySupervisorDto>? MaturityWithSupervisors { get; set; } 
        
    }
}