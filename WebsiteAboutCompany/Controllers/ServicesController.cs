using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteAboutCompany.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            int totalRecord = 0;
            var model = new ServiceDao().ListServicesPaging(ref totalRecord, page, pageSize);


            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var model = new ServiceDao().GetByID(id);
            return View(model);
        }
    }
}