using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class CbesWithSubSupervisorMetadata
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

  [MetadataType(typeof(CbesWithSubSupervisorMetadata))]
  public partial class CbesWithSubSupervisor
  {
     public static CbesWithSubSupervisor Create(CbesManagementContext db,  int CbesId ,  int? PositionId , int userid )
    {
        DateTime date = DateTime.Now;
        CbesWithSubSupervisor maturityWithSupervisor= new CbesWithSubSupervisor
        {
            CreateDate = date,
            UpdateDate = date,
            IsDeleted = false,
            CreateBy = userid,
            UpdateBy = userid,
            CbesId = CbesId,
            PositionId = PositionId,
        };
        db.CbesWithSubSupervisors.Add(maturityWithSupervisor);
        return maturityWithSupervisor ;
    }

        public static ICollection<CbesWithSubSupervisor> Update(CbesManagementContext db,  int CbesId ,ICollection<CbesWithSubSupervisor> supervisor , int userid )
    {
       DateTime date = DateTime.Now;
        
        foreach(var data in supervisor){
          if(data.Id == 0){
            CbesWithSubSupervisor result = Create(db,CbesId,data.PositionId,userid);
            db.CbesWithSubSupervisors.Add(result);
          }else{
            data.UpdateDate = date;
            data.UpdateBy = userid;
            data.Position = null;
            db.CbesWithSubSupervisors.Update(data);
          }
        }
        
        return supervisor ;
    }
  }

}