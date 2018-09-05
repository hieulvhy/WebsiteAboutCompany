using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class MenuDao
    {
        private readonly CompanyDbContext _db ;
        public MenuDao()
        {
            _db = new CompanyDbContext();
        }

        public List<Menu> ListByGroupId(int groupId)
        {
            return _db.Menus.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
