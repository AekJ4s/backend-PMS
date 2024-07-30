
using CBEsApi.Dtos.CBEsUserDto;

namespace CBEsApi.Dtos.CBEsPositionDto
{
    public class CBEsPositionDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsLastDelete { get; set; }

        public int? CreateBy { get; set; }

        public int? UpdateBy { get; set; }

        public virtual List<CbesUserDto> CbesUsers { get; set; } = new List<CbesUserDto>();
    }
}