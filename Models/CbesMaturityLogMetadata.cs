using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;

namespace CBEsApi.Models
{
    public class CbesMaturityLogMetadata
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

    public class CbesMaturityLogChang
    {
        public string? Detail { get; set; }
        public virtual ICollection<MaturityWithSupervisorLog> MaturityWithSupervisorLogs { get; set; } = new List<MaturityWithSupervisorLog>();
    }

    [MetadataType(typeof(CbesMaturityLogMetadata))]
    public partial class CbesMaturityLog
    {
        public static CbesMaturityLog Create(CbesManagementContext db, CbesProcessLog processLog,CbesMaturity mm)
        {
            DateTime dtn = DateTime.Now;
                CbesMaturityLog newMaturityLog = new CbesMaturityLog
                {
                    Detail = mm.Detail,
                    Lv = mm.Lv,
                    CreateDate = dtn,
                    UpdateDate = dtn,
                    IsDeleted = false,
                    CbesProcessLog = processLog,
                };
            return newMaturityLog;
        }

    }
}