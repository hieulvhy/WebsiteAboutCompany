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
    public class CredentialsController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: Admin/Credentials
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View(db.Credentials.ToList());
        }
        [HasCredential(RoleID = "VIEW_USER")]
        // GET: Admin/Credentials/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        // GET: Admin/Credentials/Create
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create([Bind(Include = "UserGroupID,RoleID")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Credentials.Add(credential);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(credential);
        }

        // GET: Admin/Credentials/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        // POST: Admin/Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit([Bind(Include = "UserGroupID,RoleID")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credential).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credential);
        }

        // GET: Admin/Credentials/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        // POST: Admin/Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Credential credential = db.Credentials.Find(id);
            db.Credentials.Remove(credential);
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
