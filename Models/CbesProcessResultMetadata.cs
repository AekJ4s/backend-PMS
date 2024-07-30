using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsRoleDto;
using CBEsApi.Dtos.CBEsPermissionDto;
using CBEsApi.Dtos.CBEsUserDto;
using Microsoft.Extensions.FileProviders;


namespace CBEsApi.Models
{
    public class CbesProcessResultMetadata
    {
     public int Id { get; set; }

    public int? ResultPoint { get; set; }

    public string? Remark { get; set; }

    public int? CbesProcessId { get; set; }

    public int? CbesProcessTargetId { get; set; }

    public virtual CbesProcess? CbesProcess { get; set; }

    public virtual CbesProcessTarget? CbesProcessTarget { get; set; }
    }

    [MetadataType(typeof(CbesProcessResultMetadata))]
    public partial class CbesProcessResult

    {
        public static CbesProcessResult CreateNullResult(CbesProcess cbesProcess,CbesProcessTarget cbesProcessTarget,CbesManagementContext db)
        {
            
                CbesProcessResult Result = new CbesProcessResult
                {
                    Id = 0,
                    ResultPoint = null,
                    Remark = null,
                    CbesProcessId = cbesProcess.Id,
                    CbesProcess = cbesProcess,
                    CbesProcessTargetId = cbesProcessTarget.Id,
                };
            
            return Result;
        }

        public static CbesProcessResult GetResult(CbesProcess cbesProcess,CbesProcessTarget cbesProcessTarget,int year,CbesManagementContext db)
        {
            
                CbesProcessResult Result = new CbesProcessResult
                {
                    Id = 0,
                    ResultPoint = null,
                    Remark = null,
                    CbesProcessId = cbesProcess.Id,
                    CbesProcess = cbesProcess,
                    CbesProcessTargetId = cbesProcessTarget.Id,
                    CbesProcessTarget = cbesProcessTarget
                };
                cbesProcess.CbesProcessResults.Add(Result);

            
            return Result;
        }
    }
}
