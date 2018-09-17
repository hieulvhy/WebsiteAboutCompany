using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace WebsiteAboutCompany.Areas.Admin.Controllers
{
    public class MenuTypesController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: Admin/MenuTypes
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View(db.MenuTypes.ToList());
        }

        // GET: Admin/MenuTypes/Details/5
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // GET: Admin/MenuTypes/Create
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MenuTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create([Bind(Include = "ID,Name")] MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                db.MenuTypes.Add(menuType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuType);
        }

        // GET: Admin/MenuTypes/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // POST: Admin/MenuTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit([Bind(Include = "ID,Name")] MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuType);
        }

        // GET: Admin/MenuTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // POST: Admin/MenuTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuType menuType = db.MenuTypes.Find(id);
            db.MenuTypes.Remove(menuType);
            db.SaveChanges();
            return RedirectToAction("Index");
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
