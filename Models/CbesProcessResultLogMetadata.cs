// using System.ComponentModel.DataAnnotations;
// using Microsoft.EntityFrameworkCore;
// using CBEsApi.Data;
// using CBEsApi.Dtos.CBEsRoleDto;
// using CBEsApi.Dtos.CBEsPermissionDto;
// using CBEsApi.Dtos.CBEsUserDto;
// using Microsoft.Extensions.FileProviders;


// namespace CBEsApi.Models
// {
//     public class CbesProcessTargetMetadata
//     {
//     public int Id { get; set; }

//     public int? Year { get; set; }

//     public int? TargetPoint { get; set; }

//     public int? UpdateBy { get; set; }

//     public int? CbesProcessLogId { get; set; }

//     public int? CbesTargetResultLogHeaderId { get; set; }

//     public virtual CbesProcessLog? CbesProcessLog { get; set; }

//     public virtual ICollection<CbesProcessResultLog> CbesProcessResultLogs { get; set; } = new List<CbesProcessResultLog>();

//     public virtual CbesTargetResultLogHeader? CbesTargetResultLogHeader { get; set; }

//     public virtual ICollection<CbesTargetResultLogHeader> CbesTargetResultLogHeaders { get; set; } = new List<CbesTargetResultLogHeader>();

//     public virtual CbesUser? UpdateByNavigation { get; set; }
//     }

//     [MetadataType(typeof(CbesProcessTargetMetadata))]
//     public partial class CbesProcessTarget

//     {
//         public static CbesProcessTarget CreateNullTarget(CbesProcess cbesProcess,int year,CbesManagementContext db)
//         {
            
//                 CbesProcessTarget Target = new CbesProcessTarget
//                 {
//                     Id = 0,
//                     Year = year,
//                     TargetPoint = null,
//                     CbesProcessId = cbesProcess.Id,
//                     CbesProcess = cbesProcess,
//                 };
//                 cbesProcess.CbesProcessTargets.Add(Target);
            
//             return Target;
//         }

//         public static CbesProcessTarget GetTarget(CbesProcess cbesProcess,int year,CbesManagementContext db)
//         {
            
//                 CbesProcessTarget Target = new CbesProcessTarget
//                 {
//                     Id = 0,
//                     Year = year,
//                     TargetPoint = null,
//                     CbesProcessId = cbesProcess.Id,
//                     CbesProcess = cbesProcess,
//                 };
//                 cbesProcess.CbesProcessTargets.Add(Target);
            
//             return Target;
//         }
//     }
// }
