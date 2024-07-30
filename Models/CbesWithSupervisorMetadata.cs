using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class CbesWithSupervisorMetadata
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

  [MetadataType(typeof(CbesWithSupervisorMetadata))]
  public partial class CbesWithSupervisor
  {
     public static CbeswithSupervisor Create(CbesManagementContext db, int CBEsId , int? PositionId , int userid )
    {
        DateTime date = DateTime.Now;
        CbeswithSupervisor maturityWithSupervisor= new CbeswithSupervisor
        {
            CreateDate = date,
            UpdateDate = date,
            IsDeleted = false,
            CreateBy = userid,
            UpdateBy = userid,
            CbesId = CBEsId,
            PositionId = PositionId,
        };
        return maturityWithSupervisor ;
    }

        public static ICollection<CbeswithSupervisor> Update(CbesManagementContext db,  int CbesId ,ICollection<CbeswithSupervisor> supervisor , int userid )
    {
        DateTime date = DateTime.Now;
        
        foreach(var data in supervisor){
          if(data.Id == 0){
            CbeswithSupervisor result = Create(db,CbesId,data.PositionId,userid);
            db.CbeswithSupervisors.Add(result);
          }else{
            data.UpdateDate = date;
            data.CreateDate = data.CreateDate;
            data.UpdateBy = userid;
            db.CbeswithSupervisors.Update(data);
          }
        }
        
        return supervisor ;
    }
  }

}