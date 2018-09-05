using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
