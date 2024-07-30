using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class CbesWithSubSupervisorLogMetadata
  {

    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? CbesId { get; set; }

    public int? PositionId { get; set; }

    public virtual Cbe? Cbes { get; set; }

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual CbesPosition? Position { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }

  }

  [MetadataType(typeof(CbesWithSubSupervisorLogMetadata))]
  public partial class CbesWithSubSupervisorLog
  {
     public static CbesWithSubSupervisorLog Create(CbesManagementContext db,  int CbesId ,  int? PositionId , int userid )
    {
        DateTime date = DateTime.Now;
        CbesWithSubSupervisorLog maturityWithSupervisorLog = new CbesWithSubSupervisorLog
        {
            CreateDate = date,
            UpdateDate = date,
            IsDeleted = false,
            CreateBy = userid,
            UpdateBy = userid,
            CbesLogId = CbesId,
            PositionId = PositionId,
        };
        db.CbesWithSubSupervisorLogs.Add(maturityWithSupervisorLog);
        return maturityWithSupervisorLog ;
    }

        public static ICollection<CbesWithSubSupervisorLog> Update(CbesManagementContext db,  int CbesId ,ICollection<CbesWithSubSupervisorLog> supervisor , int userid )
    {
       DateTime date = DateTime.Now;
        
        foreach(var data in supervisor){
          if(data.Id == 0){
            CbesWithSubSupervisorLog result = Create(db,CbesId,data.PositionId,userid);
            db.CbesWithSubSupervisorLogs.Add(result);
          }else{
            data.UpdateDate = date;
            data.UpdateBy = userid;
            data.Position = null;
            db.CbesWithSubSupervisorLogs.Update(data);
          }
        }
        
        return supervisor ;
    }
  }

}