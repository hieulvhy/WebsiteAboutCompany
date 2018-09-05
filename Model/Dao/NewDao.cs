using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.EF;
using Model.ViewModel;
using PagedList;

namespace Model.Dao
{
    public class NewDao
    {

        CompanyDbContext db = null;
        public NewDao()
        {
            db = new CompanyDbContext();
        }

        public IEnumerable<News> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<News> model = db.News;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<News> ListNewsPaging(ref int totalRecord,int page, int pageSize)
        {
            IEnumerable<News> model = db.News.Where(x=>x.CategoryID == 0);
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreateDate).Skip((page -1)*pageSize).Take(pageSize);
        }
        public IEnumerable<News> ListRecruitmentPaging(ref int totalRecord, int page, int pageSize)
        {
            IEnumerable<News> model = db.News.Where(x => x.CategoryID == 1);
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
        public List<NewsViewModel> GetListTopNews(int categoryId)
        {
            var listnews = from n in db.News
                           where n.CategoryID == categoryId
                           orderby n.ModifiedDate descending
                           select new NewsViewModel
                           {
                               Id = n.ID,
                               Name = n.Name,
                               Description = n.Description,
                               ModifiedDate = n.ModifiedDate
                           };
            return listnews.ToList();

        }
        public News GetByID(int id)
        {
            return db.News.Find(id);
        }
        public int Create(News news)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(news.MetaTitle))
            {
                news.MetaTitle = StringHelper.ToUnsignString(news.Name);
            }
            news.CreateDate = DateTime.Now;
            news.ModifiedDate = DateTime.Now;
            db.News.Add(news);
            db.SaveChanges();

            return news.ID;
        }
        public int Edit(News content)
        {
            try
            {
                if (string.IsNullOrEmpty(content.MetaTitle))
                {
                    content.MetaTitle = StringHelper.ToUnsignString(content.Name);
                }
                content.ModifiedDate = DateTime.Now;
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();

                return content.ID;
            }
            catch(Exception ex)
            {
                return 0;
            }
          
        }
        public bool Delete(int id)
        {
            try
            {
                var news = db.News.Find(id);
                db.News.Remove(news);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public News GetByID(long id)
        {
            return db.News.Find(id);
        }
        public bool ChangeStatus(int id)
        {
            var news = db.News.Find(id);
            news.Status = !news.Status;
            db.SaveChanges();
            return news.Status;
        }
    }
}
