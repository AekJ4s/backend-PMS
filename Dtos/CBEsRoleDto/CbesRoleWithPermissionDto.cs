using CBEsApi.Dtos.CBEsPermissionDto;

namespace CBEsApi.Dtos.CBEsRoleDto
{
    public class CbesRoleWithPermissionDto
    {
        public int Id { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsDeleted { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
        public CbesPermissionDto Permission { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
    }
}