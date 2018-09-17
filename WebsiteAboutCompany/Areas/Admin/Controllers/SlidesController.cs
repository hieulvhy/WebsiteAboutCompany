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
    public class SlidesController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: Admin/Slides
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View(db.Slides.ToList());
        }

        // GET: Admin/Slides/Details/5
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Admin/Slides/Create
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create([Bind(Include = "ID,Image,DisplayOrder,Link,Description,CreateDate,CreateBy,ModifiedDate,ModifiedBy,Status")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit([Bind(Include = "ID,Image,DisplayOrder,Link,Description,CreateDate,CreateBy,ModifiedDate,ModifiedBy,Status")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            db.Slides.Remove(slide);
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
