using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
    public class CbesProcessMetadata
    {

    }

    [MetadataType(typeof(CbesProcessMetadata))]
    public partial class CbesProcess
    {
        public static List<CbesProcess> Create(CbesManagementContext db, Cbe cbe, ICollection<CbesProcess> Pc)
        {
            DateTime dtn = DateTime.Now;
            List<CbesProcess> ListProcess = new List<CbesProcess>();
            //  Create Process
            foreach (var data in Pc)
            {
                CbesProcess Process = new CbesProcess
                {
                    Name = data.Name,
                    Weight = data.Weight,
                    CreateDate = dtn,
                    UpdateDate = dtn,
                    IsDeleted = false,
                    ProcessHeaderId = null,
                    CbesId = cbe.Id,
                };
                if (data.InverseProcessHeader.Count > 0)
                {
                    // ไปสร้าง Inverse Process 
                    List<CbesProcess> processList = CreateInverseProcessLogHeader(db, Process.Id, data.InverseProcessHeader, cbe);
                    // นำที่สร้างเสร็จแล้วกลับมาเพิ่มข้อมูลให้แก่ตัวแม่
                    foreach (var pcl in processList)
                    {
                        Process.InverseProcessHeader.Add(pcl);
                    }
                    ListProcess.Add(Process);
                }

            }
            return ListProcess;

        }

        public static List<CbesProcess> CreateInverseProcessLogHeader(CbesManagementContext db, int ProcessHeaderId, ICollection<CbesProcess> pc, Cbe cbe)
        {
            DateTime dtn = DateTime.Now;
            List<CbesProcess> ListProcess = new List<CbesProcess>();

            //  Create Process
            foreach (var data in pc)
            {
                CbesProcess Process = new CbesProcess
                {
                    Name = data.Name,
                    Weight = data.Weight,
                    CreateDate = dtn,
                    UpdateDate = dtn,
                    IsDeleted = false,
                    ProcessHeaderId = ProcessHeaderId,
                    CbesId = cbe.Id,
                    Cbes = cbe
                };
                if (data.InverseProcessHeader.Count > 0)
                {
                    List<CbesProcess> processList = CreateInverseProcessLogHeader(db, Process.Id, data.InverseProcessHeader, cbe);
                    foreach (var pcl in processList)
                    {
                        Process.InverseProcessHeader.Add(pcl);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Process.CbesMaturities.Add(CbesMaturity.Create(db, Process.Id, i + 1, 0));
                    };
                }
                ListProcess.Add(Process);
            }
            return ListProcess;
        }

        public static CBEsDto GetProcessWithTarget(int Year, CBEsDto cbe, CbesManagementContext db)
        {
            ICollection<CbesProcess> Cbesprocess = cbe.CbesProcesses;

            foreach (var process in Cbesprocess)
            {
                foreach (var ChildProcess in process.InverseProcessHeader)
                {
                    if (ChildProcess.ProcessHeaderId != null && ChildProcess.CbesMaturities.Count == 0)
                    {
                        if(ChildProcess.CbesProcessTargets.Count == 0)
                        {
                            for(int i = 0; i<3 ; i++)
                            {
                                CbesProcessTarget target = CbesProcessTarget.CreateNullTarget(ChildProcess,Year+i,db);
                                ChildProcess.CbesProcessTargets.Add(target);
                            }
                        }
                        else
                        {
                            for(int i = 0 ; i< 3 ; i++)
                            {
                                var dataTarget = db.CbesProcessTargets.Where(x => x.CbesProcessId == ChildProcess.Id && x.Year == Year+i).AsNoTracking().FirstOrDefault();
                                var dataResult = db.CbesProcessResults.Where(x => x.CbesProcessId == ChildProcess.Id && x.CbesProcessTargetId == dataTarget.Id).AsNoTracking().FirstOrDefault();
                                
                                if(dataTarget != null)
                                {
                                    db.CbesProcessTargets.Update(dataTarget);
                                    if(dataResult == null){
                                    CbesProcessResult result = CbesProcessResult.CreateNullResult(ChildProcess,dataTarget,db);
                                    ChildProcess.CbesProcessResults.Add(result);
                                    }else
                                    db.CbesProcessResults.Update(dataResult);
                                }
                                else
                                {
                                    CbesProcessTarget target = CbesProcessTarget.CreateNullTarget(ChildProcess,Year+i,db);
                                    ChildProcess.CbesProcessTargets.Add(target);
                                    CbesProcessResult result = CbesProcessResult.CreateNullResult(ChildProcess,target,db);
                                    ChildProcess.CbesProcessResults.Add(result);
                                }
                            }
                        }
                    }
                }

            }
            return cbe;
        }

        public static ICollection<CbesProcess> CreateOrUpdate(ICollection<CbesProcess> cbesProcess, CbesManagementContext db)
        {
            foreach (var dataOfProcessHeader in cbesProcess)
            {
                foreach (var process in dataOfProcessHeader.InverseProcessHeader)
                {
                    if (process.ProcessHeaderId != null && process.CbesMaturities.Count == 0)
                    {
                        foreach (var target in process.CbesProcessTargets)
                        {
                            if (target.Id == 0)
                            {
                                db.CbesProcessTargets.Add(target);
                                CbesProcessResult result = CbesProcessResult.CreateNullResult(process,target,db);
                                db.CbesProcessResults.Add(result);
                            }
                            else
                            {
                                db.CbesProcessTargets.Update(target);
                                CbesProcessResult? dataResult = db.CbesProcessResults.Where(result => result.CbesProcessTargetId == target.Id && result.Id != 0).AsNoTracking().FirstOrDefault();
                                 if(dataResult == null){
                                    CbesProcessResult result = CbesProcessResult.CreateNullResult(process,target,db);
                                    process.CbesProcessResults.Add(result);
                                    }else
                                db.CbesProcessResults.Update(dataResult);

                            }
                        }

                        db.SaveChanges();
                    }
                }
            }

            return cbesProcess;
        }
        public static ICollection<CbesProcess> GetProcessByCBEsId(int? id, CbesManagementContext db)
        {

            // นำ Process ตัวแม่ออกมาทั้งหมด
            ICollection<CbesProcess>? MainProcess = db.CbesProcesses
                                       .Where(p => p.CbesId == id && p.ProcessHeaderId == null && p.IsDeleted != true)
                                       .ToList();

            // นำ Process ตัวแม่ ไปหาตัวลูกๆ
            foreach (var HeaderProcess in MainProcess)
            {
                var dataInverse = db.CbesProcesses
                             .Where(inverse => inverse.ProcessHeaderId == HeaderProcess.Id && inverse.IsDeleted != true)
                             .Include(x => x.CbesProcessTargets)
                             .Include(r => r.CbesProcessResults)
                             .ToList();
                HeaderProcess.InverseProcessHeader = dataInverse;

                foreach (var HeaderProcessV2 in HeaderProcess.InverseProcessHeader)
                {
                    CbesProcess.GetAllInverseProcess(HeaderProcessV2, db);
                }
            }

            return MainProcess;
        }
        public static void GetAllInverseProcess(CbesProcess HeaderProcess, CbesManagementContext db)
        {

            var dataFromWhere = db.CbesProcesses.Where(i => i.ProcessHeaderId == HeaderProcess.Id && i.IsDeleted != true).Include(x => x.CbesProcessTargets).AsNoTracking().ToList();
            HeaderProcess.InverseProcessHeader = dataFromWhere;


            // ให้ process ไปหา maturity ของตนเอง
            if (dataFromWhere.Count > 0)
            {
                foreach (var data in HeaderProcess.InverseProcessHeader)
                {
                    var dataMaturity = db.CbesMaturities.Include(ms => ms.MaturityWithSupervisors).ThenInclude(ps => ps.Position).Where(i => i.CbesProcessId == data.Id && i.IsDeleted != true).AsNoTracking().ToList();
                    if (dataMaturity.Count > 0)
                    {
                        data.CbesMaturities = dataMaturity;
                    }
                }
                return;
            }
        }
        // แปลง PROCESS เป็น PROCESSDTO
        public static ICollection<CBEsProcessDto> ConvertToDto(ICollection<CbesProcess> process)
        {
            var dtoCollection = new List<CBEsProcessDto>();

            foreach (var data in process)
            {
                dtoCollection.Add(new CBEsProcessDto
                {
                    Id = data.Id,
                    Name = data.Name,
                    Weight = data.Weight,
                    IsDeleted = data.IsDeleted,
                    ProcessHeaderId = data.ProcessHeaderId,
                    CbesId = data.CbesId,
                    UpdateDate = data.UpdateDate,
                    CreateDate = data.CreateDate,
                });
            }

            return dtoCollection;
        }

        public static Cbe Update(CbesManagementContext db, Cbe cbe, int UserId)
        {
            DateTime dtn = DateTime.Now;
// ERROR
            foreach (var data in cbe.CbesProcesses)
            {
                if (data.Id == 0)
                {
                    List<CbesProcess> ListProcess = new List<CbesProcess>();
                    CbesProcess Process = new CbesProcess
                    {
                        Name = data.Name,
                        Weight = data.Weight,
                        CreateDate = dtn,
                        UpdateDate = dtn,
                        IsDeleted = false,
                        CbesId = cbe.Id,
                        Cbes = cbe
                    };
                    if (data.InverseProcessHeader.Count > 0)
                    {
                        List<CbesProcess> processList = CreateInverseProcessLogHeader(db, Process.Id, data.InverseProcessHeader, cbe);
                        foreach (var pcl in processList)
                        {
                            Process.InverseProcessHeader.Add(pcl);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Process.CbesMaturities.Add(CbesMaturity.Create(db, Process.Id, i + 1, UserId));
                        };
                    }
                    ListProcess.Add(Process);
                    db.CbesProcesses.Add(Process);
                }
                else
                {
                    CbesProcess.Updateinverse(db, data, UserId);
                    data.UpdateDate = DateTime.Now;
                }
            }
            return cbe;
        }

        public static CbesProcess Updateinverse(CbesManagementContext db, CbesProcess process, int? UserId)
        {
            foreach (var data in process.InverseProcessHeader)
            {
                CbesProcess.Updateinverse(db, data, UserId);
                foreach (var maturity in data.CbesMaturities)
                {
                    CbesMaturity.UpdateMaturity(db, maturity, UserId);
                    db.CbesMaturities.Update(maturity);

                }
                data.UpdateDate = DateTime.Now;
                db.CbesProcesses.Update(data);
            }
            return process;
        }
        public static CBEsMaturityDto ConvertMToMDto(CbesMaturity maturity)
        {
            return new CBEsMaturityDto
            {
                Id = maturity.Id,
                Detail = maturity.Detail,
                Lv = maturity.Lv,
                UpdateDate = maturity.UpdateDate,
                CreateDate = maturity.CreateDate,
                CbesProcessId = maturity.CbesProcessId,
                MaturityWithSupervisors = maturity.MaturityWithSupervisors.Select(ConvertMaturityWithSupervisor).ToList()
            };
        }
        public static CBEsMaturitySupervisorDto ConvertMaturityWithSupervisor(MaturityWithSupervisor maturityWithSupervisor)
        {
            return new CBEsMaturitySupervisorDto
            {
                Id = maturityWithSupervisor.Id,
                MaturityId = (int)maturityWithSupervisor.MaturityId,
                PositionId = maturityWithSupervisor.PositionId,
                Position = ConvertPosition(maturityWithSupervisor.Position)
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