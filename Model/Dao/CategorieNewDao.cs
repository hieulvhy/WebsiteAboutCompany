using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class CategorieNewDao
    {
        CompanyDbContext db = null;
        public CategorieNewDao()
        {
            db = new CompanyDbContext();
        }

        public List<CategoryNew> ListAll()
        {
            return db.CategoryNews.Where(x => x.Status == true).ToList();
        }

        public CategoryNew ViewDetail(long id)
        {
            return db.CategoryNews.Find(id);
        }
    }
}
