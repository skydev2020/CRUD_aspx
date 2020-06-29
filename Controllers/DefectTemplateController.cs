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
    public class DefectTemplateController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: DefectTemplate
        public ActionResult Index()
        {

            List<DefectTemplate> defectTemplates = new List<DefectTemplate>();


            var model = (from dt in defectTemplates
                         select new vmDefectTemplate()
                         {
                           DefectCost = dt.DefectCost,
                           DefectGroupId = dt.DefectGroupId,
                           DefectName = dt.DefectName,
                           DefectTemplateId = dt.DefectTemplateId,
                         });

            // GetList Of Group Names
            ViewBag.GroupNames = db.DefectGroups.ToList().OrderBy(x => x.GroupName);

            return View(db.DefectTemplates.ToList());


        }

        // GET: DefectTemplate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectTemplate defectTemplate = db.DefectTemplates.Find(id);
            if (defectTemplate == null)
            {
                return HttpNotFound();
            }
            return View(defectTemplate);
        }

        // GET: DefectTemplate/Create
        public ActionResult Create()
        {
            var defectGroup = db.DefectGroups.Where(x => x.DefectGroupId != 1).ToList();

            ViewBag.DefectGroupId = new SelectList(defectGroup, "DefectGroupId", "GroupName", "-- Select Group --");

            return View();
        }

        // POST: DefectTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectTemplateId,DefectName,DefectCost,DefectGroupId")] DefectTemplate defectTemplate, string NewGroupName = null)
        {
            // Create New Group
            if (!String.IsNullOrEmpty(NewGroupName))
            {
                int? maxDisplayOrder = db.DefectGroups.Max(d => d.DisplayOrder);
                DefectGroup group = new DefectGroup();
                group.GroupName = NewGroupName;
                group.DisplayOrder = maxDisplayOrder + 1;
                db.DefectGroups.Add(group);
                db.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                db.DefectTemplates.Add(defectTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(defectTemplate);
        }

        // GET: DefectTemplate/Edit/5
        public ActionResult Edit(int? id)
        {
            // GetList Of Group Names
            ViewBag.GroupNames = db.DefectGroups.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectTemplate defectTemplate = db.DefectTemplates.Find(id);
            if (defectTemplate == null)
            {
                return HttpNotFound();
            }
            return View(defectTemplate);
        }

        // POST: DefectTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectTemplateId,DefectName,DefectCost,DefectGroupId")] int id, string DefectName, decimal DefectCost)
        {
            DefectTemplate defectTemplate = db.DefectTemplates.Find(id);

            if (ModelState.IsValid)
            {
                defectTemplate.DefectName = DefectName;
                defectTemplate.DefectCost = DefectCost;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(defectTemplate);
        }

        // GET: DefectTemplate/Delete/5
        public ActionResult Delete(int? id)
        {
            // GetList Of Group Names
            ViewBag.GroupNames = db.DefectGroups.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectTemplate defectTemplate = db.DefectTemplates.Find(id);
            if (defectTemplate == null)
            {
                return HttpNotFound();
            }
            return View(defectTemplate);
        }

        // POST: DefectTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefectTemplate defectTemplate = db.DefectTemplates.Find(id);
            db.DefectTemplates.Remove(defectTemplate);
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
