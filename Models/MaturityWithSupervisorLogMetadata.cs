using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class MaturityWithSupervisorLogMetadata
  {

     public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }

    public int? MaturityLogId { get; set; }

    public int? CbesLogHeaderId { get; set; }

    public int? PositionId { get; set; }

    public virtual CbesLogHeader? CbesLogHeader { get; set; }

    public virtual CbesMaturityLog? MaturityLog { get; set; }

    public virtual CbesPosition? Position { get; set; }

  }

  [MetadataType(typeof(MaturityWithSupervisorLogMetadata))]
  public partial class MaturityWithSupervisorLog
  {
    public static MaturityWithSupervisorLog Create(CbesManagementContext db, int? MaturityLogId , int? PositionId , int? UserId )
    {
        DateTime date = DateTime.Now;
        MaturityWithSupervisorLog maturityWithSupervisor= new MaturityWithSupervisorLog
        {
            CreateDate = date,
            UpdateDate = date,
            IsDeleted = false,
            CreateBy = UserId,
            UpdateBy = UserId,
            MaturityLogId = MaturityLogId,
            PositionId = PositionId,
        };
        db.MaturityWithSupervisorLogs.Add(maturityWithSupervisor);
        return maturityWithSupervisor ;
    }

  }

}