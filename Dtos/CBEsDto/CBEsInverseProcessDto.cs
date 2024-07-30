using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsInverseProcessDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool? IsDeleted { get; set; }

        public int? ProcessHeaderId { get; set; }

        public virtual List<CbesProcess> InverseProcessHeader { get; set; }
        
    }
}