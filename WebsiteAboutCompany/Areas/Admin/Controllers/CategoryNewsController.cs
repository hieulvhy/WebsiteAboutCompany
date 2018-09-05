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
    public class CategoryNewsController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: Admin/CategoryNews
        public ActionResult Index()
        {
            return View(db.CategoryNews.ToList());
        }

        // GET: Admin/CategoryNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            if (categoryNew == null)
            {
                return HttpNotFound();
            }
            return View(categoryNew);
        }

        // GET: Admin/CategoryNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Detail,Status,CreateDate,CreateBy,ModifiedDate,ModifiedBy,ShowOnHome")] CategoryNew categoryNew)
        {
            if (ModelState.IsValid)
            {
                db.CategoryNews.Add(categoryNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryNew);
        }

        // GET: Admin/CategoryNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            if (categoryNew == null)
            {
                return HttpNotFound();
            }
            return View(categoryNew);
        }

        // POST: Admin/CategoryNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Detail,Status,CreateDate,CreateBy,ModifiedDate,ModifiedBy,ShowOnHome")] CategoryNew categoryNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryNew);
        }

        // GET: Admin/CategoryNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            if (categoryNew == null)
            {
                return HttpNotFound();
            }
            return View(categoryNew);
        }

        // POST: Admin/CategoryNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            db.CategoryNews.Remove(categoryNew);
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
