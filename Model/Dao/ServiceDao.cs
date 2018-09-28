using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class ServiceDao
    {
        CompanyDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ServiceDao()
        {
            db = new CompanyDbContext();
        }
        public IEnumerable<Service> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Service> model = db.Services;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Service> ListServicesPaging(ref int totalRecord, int page, int pageSize)
        {
            IEnumerable<Service> model = db.Services;
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
        public Service GetByID(int id)
        {
            return db.Services.Find(id);
        }
        public bool ChangeStatus(int id)
        {
            var service = db.Services.Find(id);
            if (service == null) return true;
            service.Status = !service.Status;
            db.SaveChanges();
            return service.Status;
        }
        public bool ChangeStatusCategory(int id)
        {
            var category = db.CategoryServices.Find(id);
            if (category == null) return true;
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var service = db.Services.Find(id);
                if (service == null) return false;
                db.Services.Remove(service);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DeleteCategory(int id)
        {
            try
            {
                var category = db.CategoryServices.Find(id);
                if (category == null) return false;
                db.CategoryServices.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
