using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace WebsiteAboutCompany.Areas.Admin.Controllers
{
    public class CategoryServicesController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: Admin/CategoryServices
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View(db.CategoryServices.ToList());
        }

        // GET: Admin/CategoryServices/Details/5
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryService categoryService = db.CategoryServices.Find(id);
            if (categoryService == null)
            {
                return HttpNotFound();
            }
            return View(categoryService);
        }

        // GET: Admin/CategoryServices/Create
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Detail,Status,CreateDate,CreateBy,ModifiedDate,ModifiedBy,ShowOnHome")] CategoryService categoryService)
        {
            if (ModelState.IsValid)
            {
                db.CategoryServices.Add(categoryService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryService);
        }

        // GET: Admin/CategoryServices/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryService categoryService = db.CategoryServices.Find(id);
            if (categoryService == null)
            {
                return HttpNotFound();
            }
            return View(categoryService);
        }

        // POST: Admin/CategoryServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Detail,Status,CreateDate,CreateBy,ModifiedDate,ModifiedBy,ShowOnHome")] CategoryService categoryService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryService);
        }

        // GET: Admin/CategoryServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryService categoryService = db.CategoryServices.Find(id);
            if (categoryService == null)
            {
                return HttpNotFound();
            }
            return View(categoryService);
        }

        // POST: Admin/CategoryServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryService categoryService = db.CategoryServices.Find(id);
            db.CategoryServices.Remove(categoryService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ServiceDao().ChangeStatusCategory(id);
            return Json(new
            {
                status = result
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
