using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace WebsiteAboutCompany.Controllers
{
    public class HomeController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();
        public ActionResult Index()
        {
            var listSerVices = db.Services.OrderByDescending(x => x.CreateDate).Take(4).ToList();
            var listNews = db.News.Where(x=>x.CategoryID == 0).OrderByDescending(x => x.CreateDate).Take(6).ToList();
            ViewBag.NewServices = listSerVices;
            ViewBag.ListNews = listNews;
            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult TopNews()
        {
            var topNews = new NewDao().GetListTopNews(1).Take(5);
            ViewBag.TopNews = topNews;
            return PartialView();
        }
        [HttpPost]
        public ActionResult SendFeedbackToDb(string name, string email, string address,string comment)
        {
            bool result;
            try
            {
                var model = new FeedbackDao();
                var feedback = new Feedback
                {
                    Name = name,
                    Email = email,
                    Address = address,
                    Content = comment,

                };
                var id = model.Create(feedback);
                result = id != 0;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet );
        }
    }
}