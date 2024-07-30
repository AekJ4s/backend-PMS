using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CBEsApi.Models
{
    public class CbeMetadata
    {
    public int Id { get; set; }

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLastDelete { get; set; }

    public int? CreateBy { get; set; }

    public virtual ICollection<CbesPlanningLog> CbesPlanningLogs { get; set; } = new List<CbesPlanningLog>();

    public virtual ICollection<CbesPlanning> CbesPlannings { get; set; } = new List<CbesPlanning>();

    public virtual ICollection<CbesProcess> CbesProcesses { get; set; } = new List<CbesProcess>();

    public virtual ICollection<CbesTargetResultLogHeader> CbesTargetResultLogHeaders { get; set; } = new List<CbesTargetResultLogHeader>();

    public virtual ICollection<CbesWithSubSupervisor> CbesWithSubSupervisors { get; set; } = new List<CbesWithSubSupervisor>();

    public virtual ICollection<CbeswithSupervisor> CbeswithSupervisors { get; set; } = new List<CbeswithSupervisor>();

    public virtual CbesUser? CreateByNavigation { get; set; }
    }

    public class CbeCreate{

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public virtual ICollection<CbesLogHeader>? CbesLogHeaders { get; set; } 

    public virtual ICollection<CbesProcess>? CbesProcesses { get; set; }

    }
    [MetadataType(typeof(CbeMetadata))]
    public partial class Cbe
    {
        public static Cbe Create(CbesManagementContext db,Cbe cbeHeader,int UserId)
        {
            if(cbeHeader !=  null){
            
                ICollection <CbesProcess> pc = cbeHeader.CbesProcesses;
                Cbe cbeOrigin = new Cbe
            {
                ThaiName = cbeHeader.ThaiName,
                EngName = cbeHeader.EngName,
                ShortName = cbeHeader.ShortName,
                IsActive = true,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                IsLastDelete = false,
                CreateBy = UserId,
            };
            // db.SaveChanges();
            List<CbesProcess> ListProcess = CbesProcess.Create(db,cbeOrigin, pc);
            foreach(var data in ListProcess){
                cbeOrigin.CbesProcesses.Add(data);
            }
            db.Cbes.Add(cbeOrigin);
            return cbeOrigin;
            }
            else{
            return null;
            }
        }
        
        public static List<CBEsDto> GetAll(CbesManagementContext db)
        {
            List<CBEsDto> cbes = db.Cbes.Where(q => q.IsDeleted != true).Select(s => new CBEsDto
            {
                Id = s.Id,
                ThaiName = s.ThaiName,
                EngName = s.EngName,
                ShortName = s.ShortName,
                IsActive = s.IsActive,
                CreateDate = s.CreateDate,
                UpdateDate = s.UpdateDate,
                IsDeleted = s.IsDeleted,
                IsLastDelete = s.IsLastDelete,
                CreateBy = s.CreateBy,
            }).ToList();

            return cbes;
        }

        public static List<CBEsDto> GetAllBin(CbesManagementContext db)
        {
            List<CBEsDto> cbes = db.Cbes.Where(q => q.IsDeleted == true).Select(s => new CBEsDto
            {
                Id = s.Id,
                ThaiName = s.ThaiName,
                EngName = s.EngName,
                ShortName = s.ShortName,
                IsActive = s.IsActive,
                CreateDate = s.CreateDate,
                UpdateDate = s.UpdateDate,
                IsDeleted = s.IsDeleted,
                IsLastDelete = s.IsLastDelete,
                CreateBy = s.CreateBy,
            }).ToList();

            return cbes;
        }

        public static Cbe Update(CbesManagementContext db,Cbe cbe,int UserId)
        {
            CbesWithSupervisor.Update(db,cbe.Id,cbe.CbeswithSupervisors,UserId);
            CbesWithSubSupervisor.Update(db,cbe.Id,cbe.CbesWithSubSupervisors,UserId);
            CbesProcess.Update(db,cbe,UserId);
            cbe.ThaiName = cbe.ThaiName;
            cbe.UpdateDate = DateTime.Now;
            db.Entry(cbe).State = EntityState.Modified; // นำเข้าฐานข้อมูล
            db.SaveChanges();
            return cbe;
            
        }

        //  public static Cbe GetRealCbe(CbesManagementContext db, int? id)
        // {
        //     if (id != null)
        //     {
        //         Cbe? cbe = db.Cbes
        //         .Include(sp => sp.CbesWithSubSupervisors)
        //         .ThenInclude(spp => spp.Position)
        //         .AsNoTracking()
        //         .Include(ssp => ssp.CbesWithSubSupervisors)
        //         .ThenInclude(sspp => sspp.Position)
        //         .AsNoTracking()
        //         .Where(q => q.Id == id)
        //         .AsNoTracking()
        //         .Select(cbes => new Cbe
        //         {
        //             Id = cbes.Id,
        //             ThaiName = cbes.ThaiName,
        //             EngName = cbes.EngName,
        //             ShortName = cbes.ShortName,
        //             IsActive = cbes.IsActive,
        //             CreateDate = cbes.CreateDate,
        //             UpdateDate = cbes.UpdateDate,
        //             IsDeleted = cbes.IsDeleted,
        //             IsLastDelete = cbes.IsLastDelete,
        //             CreateBy = cbes.CreateBy,
        //             CbeswithSupervisors = cbes.CbeswithSupervisors
        //                 .Where(x => x.CbesId == cbes.Id)
        //                 .Select(data => new CbesWithSupervisor
        //                 {
        //                     Id = data.Id,
        //                     CbesId = cbes.Id,
        //                     PositionId = data.PositionId,
        //                     Position = new CbesPosition
        //                     {
        //                         Id = data.PositionId,
        //                         Name = data.Position.Name,
        //                     }
        //                 }).ToList(),
        //             CbesWithSubSupervisors = cbes.CbesWithSubSupervisors
        //                 .Where(x => x.CbesId == cbes.Id)
        //                 .Select(data => new CbesWithSubSupervisor
        //                 {
        //                     Id = data.Id,
        //                     CbesId = cbes.Id,
        //                     PositionId = data.PositionId,
        //                     Position = new CbesPosition
        //                     {
        //                         Id = data.PositionId,                                    
        //                         Name = data.Position.Name,
        //                     }
        //                 }).ToList(),
        //         }).AsNoTracking().FirstOrDefault();
        //         var dataReturn = CbesProcess.GetProcessByCBEsId(cbe.Id,db).ToList();
        //         foreach (var data in dataReturn)
        //         {
        //         cbe.CbesProcesses.Add(data);
        //         }
        //         var maxRound = cbe.CbesLogHeaders
        //            .Where(q => q.CbesId == cbe.Id)
        //            .Max(r => r.Round);

        //         CbesLogHeader? maxRoundHeaders = cbe.CbesLogHeaders
        //                 .Where(q => q.CbesId == cbe.Id && q.Round == maxRound).FirstOrDefault();
        //         cbe.CbesLogHeaders.Add(maxRoundHeaders);
        //         return cbe ?? new Cbe();
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }
        
        public static CBEsDto GetById(CbesManagementContext db, int? id)
        {
            if (id != null)
            {
                CBEsDto? cbe = db.Cbes
                .Include(sp => sp.CbesWithSubSupervisors)
                .ThenInclude(spp => spp.Position)
                .AsNoTracking()
                .Include(ssp => ssp.CbesWithSubSupervisors)
                .ThenInclude(sspp => sspp.Position)
                .AsNoTracking()
                .Where(q => q.Id == id)
                .AsNoTracking()
                .Select(cbes => new CBEsDto
                {
                    Id = cbes.Id,
                    ThaiName = cbes.ThaiName,
                    EngName = cbes.EngName,
                    ShortName = cbes.ShortName,
                    IsActive = cbes.IsActive,
                    CreateDate = cbes.CreateDate,
                    UpdateDate = cbes.UpdateDate,
                    IsDeleted = cbes.IsDeleted,
                    IsLastDelete = cbes.IsLastDelete,
                    CreateBy = cbes.CreateBy,
                    CbeswithSupervisors = cbes.CbeswithSupervisors
                        .Where(x => x.CbesId == cbes.Id)
                        .Select(data => new CBEsWithSupervisorDto
                        {
                            Id = data.Id,
                            CbesId = cbes.Id,
                            PositionId = data.PositionId,
                            Position = new CBEsPositionDto
                            {
                                Id = data.PositionId,
                                Name = data.Position.Name,
                            }
                        }).ToList(),
                    CbesWithSubSupervisors = cbes.CbesWithSubSupervisors
                        .Where(x => x.CbesId == cbes.Id)
                        .Select(data => new CBEsWithSubSupervisorDto
                        {
                            Id = data.Id,
                            CbesId = cbes.Id,
                            PositionId = data.PositionId,
                            Position = new CBEsPositionDto
                            {
                                Id = data.PositionId,                                    
                                Name = data.Position.Name,
                            }
                        }).ToList(),
                }).AsNoTracking().FirstOrDefault();
                var dataReturn = CbesProcess.GetProcessByCBEsId(cbe.Id,db).ToList();
                foreach (var data in dataReturn)
                {
                cbe.CbesProcesses.Add(data);
                }
                var maxRound = cbe.CbesLogHeaders
                   .Where(q => q.CbesId == cbe.Id)
                   .Max(r => r.Round);

                CbesLogHeader? maxRoundHeaders = cbe.CbesLogHeaders
                        .Where(q => q.CbesId == cbe.Id && q.Round == maxRound).FirstOrDefault();
                cbe.CbesLogHeaders.Add(maxRoundHeaders ?? new CbesLogHeader{
                         Id = 0,
                         Round = 1,
                         Remark = "หมายเหตุในการแก้ไข",
                     });
                
                return cbe ?? new CBEsDto();
            }
            else
            {
                return null;
            }
        }

        public static CBEsDto Delete(CbesManagementContext db, int id)
        {
            Cbe? cbe = db.Cbes.Find(id);
            cbe.IsDeleted = true;
            CBEsDto cbeDto = new CBEsDto
            {
                Id = cbe.Id,
                ThaiName = cbe.ThaiName,
                EngName = cbe.EngName,
                ShortName = cbe.ShortName,
                IsDeleted = cbe.IsDeleted,
            };

            db.SaveChanges();
            return cbeDto;
        }

        public static CBEsDto CancelDelete(CbesManagementContext db, int id)
        {
            CBEsDto cbe = GetById(db, id);
            cbe.IsDeleted = false;

            // db.Entry(cbe).State = EntityState.Modified;
            db.SaveChanges();

            return cbe;
        }

        public static CBEsDto LastDelete(CbesManagementContext db, int id)
        {
            CBEsDto cbe = GetById(db, id);
            cbe.IsLastDelete = true;

            // db.Entry(cbe).State = EntityState.Modified;
            db.SaveChanges();

            return cbe;
        }

        public static CBEsWithPlanningDto GetAllPlanningOfCBEs(CbesManagementContext db, int id)
        {
            var cbe = db.Cbes
            .Where(cbes => cbes.Id == id)
            .Select(cbesdata => new CBEsWithPlanningDto
            {
                Id = cbesdata.Id,
                ThaiName = cbesdata.ThaiName,
                EngName = cbesdata.EngName,
                ShortName = cbesdata.ShortName,
                CbesPlannings = cbesdata.CbesPlannings
                    .Where(planning => planning.CbesId == cbesdata.Id)
                    .Select(planningdata => new CBEsPlanningDto
                    {
                        Id = planningdata.Id,
                        Name = planningdata.Name,
                        Year = planningdata.Year,
                        IsDeleted = planningdata.IsDeleted,
                        IsLastDelete = planningdata.IsLastDelete,
                        CbesId = planningdata.CbesId
                    }).ToList()
            })
            .FirstOrDefault();

            return cbe ?? new CBEsWithPlanningDto();
        }

       
    }
}
