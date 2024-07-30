using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;
using CBEsApi.Dtos.CBEsDto;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Models
{
  public class CbesLogMetadata
  {

  }

  public class CbesLogCreate
  {
    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? ShortName { get; set; }

    public virtual ICollection<CbesProcessLogCreate> CbesProcessLogs { get; set; } = new List<CbesProcessLogCreate>();

  }

  [MetadataType(typeof(CbesLogMetadata))]
  public partial class CbesLog
  {
    public static CBEsLogDto GetById(CbesManagementContext db, int? id)
    {

      CbesLog cbesLog = db.CbesLogs.Include(p => p.CbesProcessLogs).Where(x => x.Id == id).FirstOrDefault();
      if (cbesLog == null)
      {
        return new CBEsLogDto();
      }
      else
      {
        CBEsLogDto cbesLogDto = new CBEsLogDto
        {
          Id = cbesLog.Id,
          ThaiName = cbesLog.ThaiName,
          EngName = cbesLog.EngName,
          ShortName = cbesLog.ShortName,
          Year = cbesLog.Year,
          CbesProcessLogs = new List<CBEsProcessLogDto>()
        };

        var dataOfProcessLog = CbesProcessLog.GetProcessLogByCBEsId(cbesLog.Id, db).ToList();

        foreach (var data in dataOfProcessLog)
        {
          cbesLogDto.CbesProcessLogs.Add(data);
        }


        return cbesLogDto;
      };
    }

    public static CbesLog Create(CbesManagementContext db,Cbe cbeHeader,int UserId)
        {
            if(cbeHeader !=  null){
                ICollection <CbesProcess> pc = cbeHeader.CbesProcesses;
                CbesLog cbeLogs = new CbesLog
            {
                ThaiName = cbeHeader.ThaiName,
                EngName = cbeHeader.EngName,
                ShortName = cbeHeader.ShortName,
                Year = DateTime.Now.Year,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                IsLastDelete = false,
                UpdateBy = UserId,
            };
            // db.SaveChanges();
            List<CbesProcessLog> ListProcess = CbesProcessLog.Create(db,cbeLogs, pc);
            foreach(var data in ListProcess){
                cbeLogs.CbesProcessLogs.Add(data);
            }
            db.SaveChanges();
            return cbeLogs;
            }
            else{
            return null;
            }
        }

         public static CbesLog Copy(CbesManagementContext db,Cbe cbeHeader,int UserId)
        {
            if(cbeHeader !=  null){
                ICollection <CbesProcess> pc = cbeHeader.CbesProcesses;
                CbesLog cbeLogs = new CbesLog
            {
                ThaiName = cbeHeader.ThaiName,
                EngName = cbeHeader.EngName,
                ShortName = cbeHeader.ShortName,
                Year = DateTime.Now.Year,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDeleted = false,
                IsLastDelete = false,
                UpdateBy = UserId,
            };
            List<CbesProcessLog> ListProcess = CbesProcessLog.Create(db,cbeLogs, pc);
            foreach(var data in ListProcess){
                data.CbesLog = cbeLogs;
                cbeLogs.CbesProcessLogs.Add(data);
            }
            foreach(var sp in cbeHeader.CbeswithSupervisors)
            {
              CbesWithSupervisorLog newSp = new CbesWithSupervisorLog
              {
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateBy = UserId,
                UpdateBy = UserId,
                PositionId = sp.PositionId,
                CbesLogHeaderId = null,
                CbesLogId = cbeLogs.Id,
                CbesLog = cbeLogs,
              };
              cbeLogs.CbesWithSupervisorLogs.Add(newSp);
            }
             foreach(var sp in cbeHeader.CbesWithSubSupervisors)
            {
              CbesWithSubSupervisorLog newSp = new CbesWithSubSupervisorLog
              {
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateBy = UserId,
                UpdateBy = UserId,
                PositionId = sp.PositionId,
                CbesLogHeaderId = null,
                CbesLogId = cbeLogs.Id,
                CbesLog = cbeLogs,
              };
              cbeLogs.CbesWithSubSupervisorLogs.Add(newSp);
            }
            db.CbesLogs.Add(cbeLogs);
            db.SaveChanges();
            return cbeLogs;
            }
            else{
            return null;
            }
        }
        

  }

}