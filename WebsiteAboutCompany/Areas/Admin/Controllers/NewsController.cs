using Model.Dao;
using Model.EF;
using WebsiteAboutCompany.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteAboutCompany.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new NewDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_NEW")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_NEW")]
        public ActionResult Edit(int id)
        {
            var dao = new NewDao();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);

            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_NEW")]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                var dao = new NewDao();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                var id = dao.Edit(model);
                if(id != 0)
                {
                    return RedirectToAction("Index");
                }
                
            }
            SetViewBag(model.CategoryID);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_NEW")]
        public ActionResult Create(News model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreateBy = session.UserName;
                new NewDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }


        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategorieNewDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HasCredential(RoleID = "DELETE_NEW")]
        public ActionResult Delete(int id)
        {
            new NewDao().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_NEW")]
        public JsonResult ChangeStatus(int id)
        {
            var result = new NewDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}