using CBEsApi.Models;

namespace CBEsApi.Dtos.CBEsDto
{
    public class CBEsMaturitySupervisorDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int MaturityId { get; set; }
        public int? PositionId { get; set; }
        public virtual CBEsPositionDto? Position { get; set; }
    }

}
