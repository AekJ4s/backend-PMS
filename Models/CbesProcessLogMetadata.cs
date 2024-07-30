using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
    public class CbesProcessLogMetadata
    {
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Weight { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ProcessLogHeaderId { get; set; }

    public int? CbesLogId { get; set; }

    public virtual CbesLog? CbesLog { get; set; }

    public virtual ICollection<CbesMaturityLog> CbesMaturityLogs { get; set; } = new List<CbesMaturityLog>();

    public virtual ICollection<CbesProcessResultLog> CbesProcessResultLogs { get; set; } = new List<CbesProcessResultLog>();

    public virtual ICollection<CbesProcessTargetLog> CbesProcessTargetLogs { get; set; } = new List<CbesProcessTargetLog>();

    public virtual ICollection<CbesProcessLog> InverseProcessLogHeader { get; set; } = new List<CbesProcessLog>();

    public virtual CbesProcessLog? ProcessLogHeader { get; set; }
    }

    public class CbesProcessLogCreate
    {
    public string? Name { get; set; }

    public decimal? Weight { get; set; }

    public virtual ICollection<CbesProcessLog> InverseProcessLogHeader { get; set; } = new List<CbesProcessLog>();

    }
    [MetadataType(typeof(CbesProcessLogMetadata))]
    public partial class CbesProcessLog
    {
        public static List<CbesProcessLog> Create(CbesManagementContext db, CbesLog cbesLog, ICollection<CbesProcess> Pc)
        {
            DateTime dtn = DateTime.Now;
            List<CbesProcessLog> ListProcessLogs = new List<CbesProcessLog>();
            //  Create Process
            foreach (var data in Pc)
            {
                CbesProcessLog ProcessLogs = new CbesProcessLog
                {
                    Name = data.Name,
                    Weight = data.Weight,
                    CreateDate = dtn,
                    UpdateDate = dtn,
                    IsDeleted = false,
                    ProcessLogHeaderId = null,
                    CbesLogId = cbesLog.Id,
                    CbesLog = cbesLog
                };
                db.CbesProcessLogs.Add(ProcessLogs);
                if (data.InverseProcessHeader.Count > 0)
                {
                    List<CbesProcessLog> processList = CreateInverseProcessLogHeader(db, ProcessLogs.Id, data.InverseProcessHeader , ProcessLogs, cbesLog);
                    foreach (var pcl in processList)
                    {
                        ProcessLogs.InverseProcessLogHeader.Add(pcl);
                    }
                    ListProcessLogs.Add(ProcessLogs);
                }

            }
            return ListProcessLogs;

        }

        public static List<CbesProcessLog> CreateInverseProcessLogHeader(CbesManagementContext db, int ProcessHeaderId, ICollection<CbesProcess> pc,CbesProcessLog ProcessHeader,CbesLog cbesLog)
        {
            DateTime dtn = DateTime.Now;
            List<CbesProcessLog> ListProcessLogs = new List<CbesProcessLog>();

            //  Create Process
            foreach (var data in pc)
            {
                CbesProcessLog ProcessLogs = new CbesProcessLog
                {
                    Name = data.Name,
                    Weight = data.Weight,
                    CreateDate = dtn,
                    UpdateDate = dtn,
                    IsDeleted = false,
                    ProcessLogHeaderId = ProcessHeaderId,
                    ProcessLogHeader = ProcessHeader,
                    CbesLog = cbesLog,
                    CbesLogId = cbesLog.Id

                };
                if (data.InverseProcessHeader.Count > 0)
                {
                    List<CbesProcessLog> processList = CreateInverseProcessLogHeader(db, ProcessLogs.Id, data.InverseProcessHeader,ProcessLogs,cbesLog);
                    foreach (var pcl in processList)
                    {
                        pcl.CbesLog = cbesLog;
                        ProcessLogs.InverseProcessLogHeader.Add(pcl);
                    }
                }
                else
                {
                    foreach (var ma in data.CbesMaturities)
                    {
                        ProcessLogs.CbesMaturityLogs.Add(CbesMaturityLog.Create(db, ProcessLogs, ma));
                    };
                }
                ListProcessLogs.Add(ProcessLogs);
            }
            return ListProcessLogs;
        }
        public static ICollection<CBEsProcessLogDto> GetProcessLogByCBEsId(int id, CbesManagementContext db)
        {

            // นำ Process ตัวแม่ออกมาทั้งหมด
            ICollection<CbesProcessLog>? MainProcess = db.CbesProcessLogs
                                       .Where(p => p.CbesLogId == id && p.ProcessLogHeaderId == null && p.IsDeleted != true)
                                       .ToList();

            // นำ Process ตัวแม่ไป Convert ให้เป็น process DTO
            ICollection<CBEsProcessLogDto>? MainProcessDto = ConvertToDto(MainProcess);

            // นำ Process ตัวแม่ ไปหาตัวลูกๆ
            foreach (var HeaderProcess in MainProcessDto)
            {
                var dataInverse = db.CbesProcessLogs
                             .Where(inverse => inverse.ProcessLogHeaderId == HeaderProcess.Id && inverse.IsDeleted != true)
                             .ToList();
                HeaderProcess.InverseProcessLogHeader = ConvertToDto(dataInverse);

                foreach (var HeaderProcessV2 in HeaderProcess.InverseProcessLogHeader)
                {
                    CbesProcessLog.GetAllInverseProcess(HeaderProcessV2, db);
                }
            }

            return MainProcessDto;
        }
        public static void GetAllInverseProcess(CBEsProcessLogDto HeaderProcess, CbesManagementContext db)
        {

            var dataFromWhere = db.CbesProcessLogs.Where(i => i.ProcessLogHeaderId == HeaderProcess.Id && i.IsDeleted != true).AsNoTracking().ToList();
            HeaderProcess.InverseProcessLogHeader = ConvertToDto(dataFromWhere).ToList();
           
           
           // ให้ process ไปหา maturity ของตนเอง
            if (dataFromWhere.Count > 0)
            {
                foreach (var data in HeaderProcess.InverseProcessLogHeader)
                {
                    var dataMaturity = db.CbesMaturityLogs.Include(ms => ms.MaturityWithSupervisorLogs).ThenInclude(ps => ps.Position).Where(i => i.CbesProcessLogId == data.Id && i.IsDeleted != true).AsNoTracking().ToList();
                    if (dataMaturity.Count > 0)
                    {
                        data.CbesMaturityLogs = dataMaturity.Select(ConvertMToMDto).ToList();
                    }
                }
                    return;
            }
        }
        // แปลง PROCESS เป็น PROCESSDTO
        public static ICollection<CBEsProcessLogDto> ConvertToDto(ICollection<CbesProcessLog> process)
        {
            var dtoCollection = new List<CBEsProcessLogDto>();

            foreach (var data in process)
            {
                dtoCollection.Add(new CBEsProcessLogDto
                {
                    Id = data.Id,
                    Name = data.Name,
                    Weight = data.Weight,
                    IsDeleted = data.IsDeleted,
                    ProcessLogHeaderId = data.ProcessLogHeaderId,
                    CbesLogId = data.CbesLogId,
                });
            }

            return dtoCollection;
        }
        public static CBEsMaturityLogDto ConvertMToMDto(CbesMaturityLog maturity)
        {
            return new CBEsMaturityLogDto
            {
                Id = maturity.Id,
                Detail = maturity.Detail,
                Lv = maturity.Lv,
                MaturityWithSupervisorLogs = maturity.MaturityWithSupervisorLogs.Select(ConvertMaturityWithSupervisor).ToList()
            };
        }
        public static CBEsMaturitySupervisorLogsDto ConvertMaturityWithSupervisor(MaturityWithSupervisorLog maturityWithSupervisorLogs)
        {
            return new CBEsMaturitySupervisorLogsDto
            {
                Id = maturityWithSupervisorLogs.Id,
                MaturityLogId = (int)maturityWithSupervisorLogs.MaturityLogId,
                PositionId = maturityWithSupervisorLogs.PositionId,
                Position = ConvertPosition(maturityWithSupervisorLogs.Position),
                IsDeleted = maturityWithSupervisorLogs.IsDeleted,
                CbesLogHeaderId = maturityWithSupervisorLogs.CbesLogHeaderId,
            };
        }
        public static CBEsPositionDto ConvertPosition(CbesPosition position)
        {
            return new CBEsPositionDto
            {
                Id = position.Id,
                Name = position.Name,
            };
        }


    }
}