using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using CBEsApi.Dtos.CBEsUserDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
    public class CbesLogHeaderMetadata
    {

    }

    public class CbesLogHeaderCreate
    {
        public string? Remark { get; set; }
        public virtual CbeCreate? Cbes { get; set; }
        public virtual CbesUser? UpdateByNavigation { get; set; }
    }



    [MetadataType(typeof(CbesLogHeaderMetadata))]
    public partial class CbesLogHeader
    {
        public static List<CbesLogHeaderDto> GetAll(CbesManagementContext db, int id)
        {
            List<CbesLogHeaderDto>? cbe = db.CbesLogHeaders.Where(q => q.CbesId == id && q.IsDeleted != true)
                                                        .Include(t => t.CbesLogType)
                                                        .Select(s => new CbesLogHeaderDto
                                                        {
                                                            Id = s.Id,
                                                            Round = s.Round,                  
                                                            Remark = s.Remark,
                                                            UpdateDate = s.UpdateDate,
                                                            IsDeleted = s.IsDeleted,
                                                            CbesLogTypeId = s.CbesLogTypeId,
                                                            CbesId = s.CbesId,
                                                            CbesLogType = s.CbesLogType,
                                                            UpdateBy = s.UpdateBy,
                                                            UpdateByNavigation = CbesUser.UserDtoGetById(db, s.UpdateBy),
                                                        }).ToList();
            return cbe;
        }
        public static CbesLogHeaderDto GetCBEsHeaderWithLog(CbesManagementContext db, int id)
        {
            CbesLogHeaderDto? CbeLogHeader = db.CbesLogHeaders.Where(q => q.Id == id && q.IsDeleted != true)
                                                        .Include(log => log.CbesLog)
                                                        .Select(s => new CbesLogHeaderDto
                                                        {
                                                            Id = s.Id,
                                                            Round = s.Round,
                                                            Remark = s.Remark,
                                                            UpdateDate = s.UpdateDate,
                                                            IsDeleted = s.IsDeleted,
                                                            CbesLogTypeId = null,
                                                            CbesId = s.CbesId,
                                                            CbesLogType = s.CbesLogType,
                                                            CbesLogId = s.CbesLogId,
                                                            UpdateBy = s.UpdateBy,
                                                            UpdateByNavigation = CbesUser.UserDtoGetById(db, s.UpdateBy),
                                                            CbesLog = CbesLog.GetById(db, s.CbesLogId)
                                                        }).FirstOrDefault();
            return CbeLogHeader;
        }

        public static CbesLogHeader Create(CbesManagementContext db, Cbe dataFromFrontEnd, Cbe cbeOrigin, CbesLog cbeLogs, int UserId)
        {
            
            CbesLogHeader? header = dataFromFrontEnd.CbesLogHeaders?.LastOrDefault() ?? new CbesLogHeader();
            
            CbesLogHeader LogHeader = new CbesLogHeader
            {
                Round = header.Round ?? 1,
                Remark = header.Remark ?? "ไม่ได้ใส่หมายเหตุ",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                UpdateBy = UserId,
                CbesId = cbeOrigin.Id,
                CbesLogId = cbeLogs.Id,
                CbesLogTypeId = 4,
                CbesLog = cbeLogs
            };
            db.CbesLogHeaders.Add(LogHeader);
            db.SaveChanges();
            return LogHeader;
        }

        public static CbesLogHeader CreateUpdateLog(CbesManagementContext db, Cbe cbeOrigin, CbesLog cbeLogs, int UserId)
        {

            CbesLogHeader? LogCome = cbeOrigin.CbesLogHeaders.OrderBy(dt => dt.UpdateDate).LastOrDefault();

            CbesLogHeader LogHeader = new CbesLogHeader
            {
                Round = LogCome.Round ?? 1,
                Remark = LogCome.Remark ?? "ไม่ได้ใส่หมายเหตุ",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                UpdateBy = UserId,
                CbesId = cbeOrigin.Id,
                CbesLogTypeId = 3,
                CbesLog = cbeLogs, // ต้องประกาศหากเป็นตัวใหม่ เพราะจะอิงย้อนกลับไปหาไม่เจอ
            };
            db.CbesLogHeaders.Add(LogHeader);
            db.SaveChanges();
            return LogHeader;
        }

        public static CbesLogHeader CreateUpdateSupervisorLog(CbesManagementContext db, Cbe cbeOrigin, CbesLog cbeLogs, int UserId)
        {

            CbesLogHeader? LogCome = cbeOrigin.CbesLogHeaders.OrderBy(dt => dt.UpdateDate).LastOrDefault();

            CbesLogHeader LogHeader = new CbesLogHeader
            {
                Round = LogCome.Round ?? 1,
                Remark = LogCome.Remark ?? "ไม่ได้ใส่หมายเหตุ",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                UpdateBy = UserId,
                CbesId = cbeOrigin.Id,
                CbesLogId = cbeLogs.Id,
                CbesLogTypeId = 3,
                CbesLog = cbeLogs // ต้องประกาศหากเป็นตัวใหม่ เพราะจะอิงย้อนกลับไปหาไม่เจอ
            };
            db.CbesLogHeaders.Add(LogHeader);
            db.SaveChanges();
            return LogHeader;
        }

            public static CbesLogHeader UpdateTarget(CbesManagementContext db, Cbe cbeOrigin, CbesLog cbeLogs, int UserId)
        {

            CbesLogHeader LogCome = cbeOrigin.CbesLogHeaders.OrderBy(dt => dt.UpdateDate).LastOrDefault();

            CbesLogHeader LogHeader = new CbesLogHeader
            {
                Round = LogCome.Round ?? 1,
                Remark = LogCome.Remark ?? "ไม่ได้ใส่หมายเหตุ",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                UpdateBy = UserId,
                CbesId = cbeOrigin.Id,
                CbesLogId = cbeLogs.Id,
                CbesLogTypeId = 7,
                CbesLog = cbeLogs // ต้องประกาศหากเป็นตัวใหม่ เพราะจะอิงย้อนกลับไปหาไม่เจอ
            };
            db.CbesLogHeaders.Add(LogHeader);
            db.SaveChanges();
            return LogHeader;
        }


        
        public static CBEsDto GetCBEs(CbesManagementContext db, int id)
        {
            if (id != null)
            {
                CBEsDto? cbe = db.Cbes
                .Include(sp => sp.CbesWithSubSupervisors)
                .ThenInclude(spp => spp.Position)
                .ThenInclude(ssp => ssp.CbesWithSubSupervisors)
                .ThenInclude(sspp => sspp.Position)
                .Where(q => q.Id == id)
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
                var dataReturn = CbesProcess.GetProcessByCBEsId(cbe.Id, db).ToList();
                foreach (var data in dataReturn)
                {
                    cbe.CbesProcesses.Add(data);
                }
                var FindmaxRound = db.CbesLogHeaders
                   .Where(q => q.CbesId == cbe.Id && q.IsDeleted != true);
                    int? Max = 0;
                    //หาค่าสูงสุด
                   foreach (var data in FindmaxRound)
                   {
                    if(data.Round > Max){
                        Max = data.Round;
                    }else Max = Max;
                   }

                CbesLogHeader? maxRoundHeaders = cbe.CbesLogHeaders
                        .Where(q => q.CbesId == cbe.Id && q.Round == Max).FirstOrDefault();
                cbe.CbesLogHeaders.Add(new CbesLogHeader {
                    Round = Max,
                    Remark = "ระบุก่อนแก้ไข",
                    CbesId = id,
                });

                return cbe ?? new CBEsDto();
            }
            else
            {
                return null;
            }
        }

        public static CbesLogHeader GetLogHeader(int id, CbesManagementContext db)
        {
            CbesLogHeader returnThis = new CbesLogHeader();
            returnThis = db.CbesLogHeaders.Where(x => x.CbesId == id).OrderBy(d => d.UpdateDate).LastOrDefault();

            return returnThis ;
        }
    }


}