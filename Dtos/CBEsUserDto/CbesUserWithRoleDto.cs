namespace CBEsApi.Dtos.CBEsUserDto
{
    public class CbesUserWithRoleDto
    {
        public int ID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public CbesUserDto? User { get; set; } // Change Users from List<UserDto> to UserDto
    }
}