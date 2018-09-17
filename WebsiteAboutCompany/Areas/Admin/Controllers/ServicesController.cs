using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PagedList;

namespace WebsiteAboutCompany.Areas.Admin.Controllers
{
    public class ServicesController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: Admin/Services
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            
            IEnumerable<Service> list = db.Services.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString)).ToList();
            }
            ViewBag.SearchString = searchString;
            return View(list);
        }

        // GET: Admin/Services/Details/5
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Admin/Services/Create
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: Admin/Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Image,Detail,Status,CreateDate,CreateBy,ModifiedDate,ModifiedBy,CategoryServicesID")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.CreateDate = DateTime.Now;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Admin/Services/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Service service = db.Services.Find(id);
            SetViewBag(service.CategoryServicesID ?? 0);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Admin/Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Image,Detail,Status,CreateDate,CreateBy,ModifiedDate,ModifiedBy,CategoryServicesID")] Service service)
        {
            if (ModelState.IsValid)
            {
                SetViewBag(service.CategoryServicesID);
                service.ModifiedDate = DateTime.Now;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Admin/Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
        public void SetViewBag(int? selectedId = null)
        {
            var cate = db.CategoryServices.Where(x => x.Status == true).ToList();
            ViewBag.CategoryServicesID = new SelectList(cate, "ID", "Name", selectedId);
        }
    }
}
