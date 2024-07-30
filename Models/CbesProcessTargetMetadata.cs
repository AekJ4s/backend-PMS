using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsRoleDto;
using CBEsApi.Dtos.CBEsPermissionDto;
using CBEsApi.Dtos.CBEsUserDto;
using Microsoft.Extensions.FileProviders;


namespace CBEsApi.Models
{
    public class CbesProcessTargetMetadata
    {
   public int Id { get; set; }

    public int? Year { get; set; }

    public int? TargetPoint { get; set; }

    public int? CbesProcessId { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }

    public virtual ICollection<CbesProcessResult> CbesProcessResults { get; set; } = new List<CbesProcessResult>();
    }

    [MetadataType(typeof(CbesProcessTargetMetadata))]
    public partial class CbesProcessTarget

    {
        public static CbesProcessTarget CreateNullTarget(CbesProcess cbesProcess,int year,CbesManagementContext db)
        {
            
                CbesProcessTarget Target = new CbesProcessTarget
                {
                    Id = 0,
                    Year = year,
                    TargetPoint = null,
                    CbesProcessId = cbesProcess.Id,
                    CbesProcess = cbesProcess,
                };

             
            return Target;
        }

        public static CbesProcessTarget GetTarget(CbesProcess cbesProcess,int year,CbesManagementContext db)
        {
            
                CbesProcessTarget Target = new CbesProcessTarget
                {
                    Id = 0,
                    Year = year,
                    TargetPoint = null,
                    CbesProcessId = cbesProcess.Id,
                    CbesProcess = cbesProcess,
                };
                cbesProcess.CbesProcessTargets.Add(Target);
            
            return Target;
        }
    }
}
