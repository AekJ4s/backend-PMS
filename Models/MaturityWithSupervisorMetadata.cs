using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class MaturityWithSupervisorMetadata
  {

    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? MaturityId { get; set; }

    public int? PositionId { get; set; }

    public virtual CbesUser? CreateByNavigation { get; set; }

    public virtual CbesMaturity? Maturity { get; set; }

    public virtual CbesPosition? Position { get; set; }

    public virtual CbesUser? UpdateByNavigation { get; set; }

  }

  [MetadataType(typeof(MaturityWithSupervisorMetadata))]
  public partial class MaturityWithSupervisor
  {
    public static MaturityWithSupervisor Create(CbesManagementContext db, int? MaturityId , int? PositionId , int? UserId )
    {
        DateTime date = DateTime.Now;
        MaturityWithSupervisor maturityWithSupervisor= new MaturityWithSupervisor
        {
            CreateDate = date,
            UpdateDate = date,
            IsDeleted = false,
            CreateBy = UserId,
            UpdateBy = UserId,
            MaturityId = MaturityId,
            PositionId = PositionId,
        };
        db.MaturityWithSupervisors.Add(maturityWithSupervisor);
        return maturityWithSupervisor ;
    }

  }

}