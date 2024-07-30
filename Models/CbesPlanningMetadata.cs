using System.ComponentModel.DataAnnotations;
using CBEsApi.Data;

namespace CBEsApi.Models
{
    public class CbesPlanningMetadata
    {

    }

    [MetadataType(typeof(CbesPlanningMetadata))]
    public partial class CbesPlanning
    {
        public static CbesPlanning Create(CbesManagementContext db, CbesPlanning permission)
        {
            permission.CreateDate = DateTime.Now;
            permission.UpdateDate = DateTime.Now;
            permission.IsDeleted = false;
            db.CbesPlannings.Add(permission);
            db.SaveChanges();

            return permission;
        }
        public static List<CbesPlanning> GetAllPlanningByCBEsId(CbesManagementContext db, int id)
        {
            List<CbesPlanning> planning = db.CbesPlannings.Where(q => q.IsDeleted != true && q.CbesId == id).ToList();
            return planning;
        }

        public static CbesPlanning GetById(CbesManagementContext db, int id)
        {
            CbesPlanning? planning = db.CbesPlannings.Where(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            return planning ?? new CbesPlanning();
        }

    }
}