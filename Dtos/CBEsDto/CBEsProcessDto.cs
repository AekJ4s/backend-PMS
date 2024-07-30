using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsProcessDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Weight { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? ProcessHeaderId { get; set; }

        public int? CbesId { get; set; }

        public virtual List<CBEsMaturityDto> CbesMaturities { get; set; } = new List<CBEsMaturityDto>();

        public virtual CBEsProcessDto? ProcessHeader { get; set; } 
        
        public virtual ICollection<CBEsProcessDto>? InverseProcessHeader { get; set; } = new List<CBEsProcessDto>();
        


    }
}