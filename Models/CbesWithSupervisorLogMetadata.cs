using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class CbesWithSupervisorLogMetadata
  {
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? CbesLogId { get; set; }

    public int? PositionId { get; set; }

    public int? CbesLogHeaderId { get; set; }

    public virtual CbesLog? CbesLog { get; set; }

    public virtual CbesLogHeader? CbesLogHeader { get; set; }

    public virtual CbesPosition? Position { get; set; }

  }

  [MetadataType(typeof(CbesWithSupervisorLogMetadata))]
  public partial class CbesWithSupervisorLog
  {
     public static CbesWithSupervisorLog Create(CbesManagementContext db, int CBEsId , int? PositionId , int userid )
    {
        DateTime date = DateTime.Now;
        CbesWithSupervisorLog cbesWithSupervisorLog = new CbesWithSupervisorLog
        {
            CreateDate = date,
            UpdateDate = date,
            IsDeleted = false,
            CreateBy = userid,
            UpdateBy = userid,
            CbesLogId = CBEsId,
            PositionId = PositionId,
        };
        return cbesWithSupervisorLog ;
    }

        public static ICollection<CbesWithSupervisorLog> Update(CbesManagementContext db,  int CbesId ,ICollection<CbesWithSupervisorLog> supervisor , int userid )
    {
        DateTime date = DateTime.Now;
        
        foreach(var data in supervisor){
          if(data.Id == 0){
            CbesWithSupervisorLog result = Create(db,CbesId,data.PositionId,userid);
            db.CbesWithSupervisorLogs.Add(result);
          }else{
            data.UpdateDate = date;
            data.CreateDate = data.CreateDate;
            data.UpdateBy = userid;
            db.CbesWithSupervisorLogs.Update(data);
          }
        }
        
        return supervisor ;
    }
  }

}