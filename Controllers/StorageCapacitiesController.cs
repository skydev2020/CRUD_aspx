using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF_CRUD.Models;
using EF_CRUD.View_Models;

namespace EF_CRUD.Controllers
{
    [Authorize]
    public class StorageCapacitiesController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: StorageCapacities
        // test
        public ActionResult Index()
        {

            List<StorageCapacity> storageCapacities = new List<StorageCapacity>();


            var model = (from sc in storageCapacities
                         select new vmStorageCapacity()
                         {
                             Description = sc.Description,
                             StorageCapacity1 = sc.StorageCapacity1,
                             StorageCapacityId = sc.StorageCapacityId
                         });

            return View(db.StorageCapacities.ToList());
        }

        // GET: StorageCapacities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCapacity storageCapacity = db.StorageCapacities.Find(id);
            if (storageCapacity == null)
            {
                return HttpNotFound();
            }
            return View(storageCapacity);
        }

        // GET: StorageCapacities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StorageCapacities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StorageCapacityId,StorageCapacity1")] StorageCapacity storageCapacity)
        {
            storageCapacity.Description = storageCapacity.StorageCapacity1 + Request["Description"];
            db.StorageCapacities.Add(storageCapacity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: StorageCapacities/Edit/5
        public ActionResult Edit(int? id)
        {
            StorageCapacity type = db.StorageCapacities.Find(id);
            ViewBag.Desc = type.Description;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCapacity storageCapacity = db.StorageCapacities.Find(id);
            if (storageCapacity == null)
            {
                return HttpNotFound();
            }
            return View(storageCapacity);
        }

        // POST: StorageCapacities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StorageCapacityId,StorageCapacity1")] StorageCapacity storageCapacity)
        {
            storageCapacity.Description = storageCapacity.StorageCapacity1 + Request["Description"];
            db.Entry(storageCapacity).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: StorageCapacities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCapacity storageCapacity = db.StorageCapacities.Find(id);
            if (storageCapacity == null)
            {
                return HttpNotFound();
            }
            return View(storageCapacity);
        }

        // POST: StorageCapacities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorageCapacity storageCapacity = db.StorageCapacities.Find(id);
            db.StorageCapacities.Remove(storageCapacity);
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
