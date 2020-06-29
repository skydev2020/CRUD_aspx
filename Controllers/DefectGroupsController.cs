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
    public class DefectGroupsController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: DefectGroups
        public ActionResult Index()
        {
            List<DefectGroup> defectGroup = new List<DefectGroup>();

            var model = (from dg in defectGroup
                         select new vmDefectGroup()
                         {
                             DefectGroupId = dg.DefectGroupId,
                             DisplayOrder = dg.DisplayOrder,
                             GroupName = dg.GroupName,
                             Info = dg.Info,
                         }) ;

            return View(db.DefectGroups.ToList());
        }

        // GET: DefectGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectGroup defectGroup = db.DefectGroups.Find(id);
            if (defectGroup == null)
            {
                return HttpNotFound();
            }
            return View(defectGroup);
        }

        // GET: DefectGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DefectGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectGroupId,GroupName,DisplayOrder,Info")] DefectGroup defectGroup)
        {
            if (ModelState.IsValid)
            {
                db.DefectGroups.Add(defectGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(defectGroup);
        }

        // GET: DefectGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectGroup defectGroup = db.DefectGroups.Find(id);
            if (defectGroup == null)
            {
                return HttpNotFound();
            }
            return View(defectGroup);
        }

        // POST: DefectGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectGroupId,GroupName,DisplayOrder,Info")] DefectGroup defectGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(defectGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(defectGroup);
        }

        // GET: DefectGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectGroup defectGroup = db.DefectGroups.Find(id);
            if (defectGroup == null)
            {
                return HttpNotFound();
            }
            return View(defectGroup);
        }

        // POST: DefectGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefectGroup defectGroup = db.DefectGroups.Find(id);
            db.DefectGroups.Remove(defectGroup);
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
