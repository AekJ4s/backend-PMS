using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;

namespace CBEsApi.Models
{
    public class CbesMaturityMetadata
    {
        public int Id { get; set; }

        public string? Detail { get; set; }

        public int? Lv { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CbesProcessLogId { get; set; }

        public virtual CbesProcessLog? CbesProcessLog { get; set; }

        public virtual ICollection<MaturityWithSupervisorLog> MaturityWithSupervisorLogs { get; set; } = new List<MaturityWithSupervisorLog>();
    }

    public class CbesMaturityChang
    {
        public string? Detail { get; set; }
        public virtual ICollection<MaturityWithSupervisorLog> MaturityWithSupervisorLogs { get; set; } = new List<MaturityWithSupervisorLog>();
    }

    [MetadataType(typeof(CbesMaturityMetadata))]
    public partial class CbesMaturity
    {
        public static CbesMaturity Create(CbesManagementContext db, int ProcessId,int lv,int UserId)
        {
            DateTime dtn = DateTime.Now;
                CbesMaturity newMaturity = new CbesMaturity
                {
                    Detail = "กำหนดรายละเอียด",
                    Lv = lv,
                    CreateDate = dtn,
                    UpdateDate = dtn,
                    IsDeleted = false,
                    CbesProcessId = ProcessId,
                };
            return newMaturity;
        }

        public static CbesMaturity UpdateMaturity(CbesManagementContext db,CbesMaturity maturity,int? UserId)
        {
            int a = 0;
            foreach (var m in maturity.MaturityWithSupervisors)
            {
                if(m.Id == 0)
                {
                    m.Position = null ;
                    m.UpdateBy = UserId;
                    m.CreateBy = UserId;
                    db.MaturityWithSupervisors.Add(m);

                }
                else
                {
                    m.Position = null ;
                    m.UpdateBy = UserId;
                    maturity.UpdateDate = DateTime.Now;
                    a = a+1;
                    db.MaturityWithSupervisors.Update(m);
                }
            }
            return maturity;
        }

    }
}