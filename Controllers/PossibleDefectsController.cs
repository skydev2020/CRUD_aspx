using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EF_CRUD.Models;
using EF_CRUD.View_Models;

namespace EF_CRUD.Controllers
{
    [Authorize]
    public class PossibleDefectsController : Controller
    {
        private CellableEntities db = new CellableEntities();
        private string webImageLocation = WebConfigurationManager.AppSettings["WebImageLocation"];

        // GET: PossibleDefects
        // test
        public ActionResult Index(int? VersionId)
        {

            List<PossibleDefect> possibleDefects = new List<PossibleDefect>();


            var model = (from pd in possibleDefects
                         select new vmPossibleDefects()
                         {
                             DefectCost = pd.DefectCost,
                             DefectGroupId = pd.DefectGroupId,
                             DefectName = pd.DefectName,
                             PossibleDefectId = pd.PossibleDefectId
                         });






            ViewBag.VersionId = new SelectList(db.PhoneVersions, "VersionId", "Version");


            if (VersionId != null)
            {
                possibleDefects = db.PossibleDefects.Where(x => x.VersionId == VersionId).ToList();
            }
            else
            {
                possibleDefects = db.PossibleDefects.ToList();
            }

            return View(possibleDefects);
        }

        // GET: PossibleDefects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleDefect possibleDefect = db.PossibleDefects.Find(id);
            if (possibleDefect == null)
            {
                return HttpNotFound();
            }
            return View(possibleDefect);
        }

        // GET: PossibleDefects/Create
        public ActionResult Create()
        {
            var defectGroup = db.DefectGroups.Where(x => x.DefectGroupId != 1).ToList();

            ViewBag.DefectGroup = new SelectList(defectGroup, "DefectGroupId", "GroupName", "-- Select Group --");
            ViewBag.VersionId = new SelectList(db.PhoneVersions, "VersionId", "Version");
            ViewBag.imageLocation = webImageLocation;

            return View();
        }

        // POST: PossibleDefects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectGroupId,NewGroupName,PossibleDefectId,DefectName,DefectCost,VersionId")] PossibleDefect possibleDefect, int? DefectGroup, string NewGroupName = null)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    int? groupId = DefectGroup;

                    // Create New Group
                    if (!String.IsNullOrEmpty(NewGroupName))
                    {
                        int? maxDisplayOrder = db.DefectGroups.Max(d => d.DisplayOrder);
                        DefectGroup group = new DefectGroup();
                        group.GroupName = NewGroupName;
                        group.DisplayOrder = maxDisplayOrder + 1;
                        db.DefectGroups.Add(group);
                        db.SaveChanges();
                        groupId = group.DefectGroupId;
                    }

                    // Add the Defect
                    possibleDefect.DefectGroupId = groupId;
                    db.PossibleDefects.Add(possibleDefect);
                    db.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "An error was encountered while attempting to complete your transaction. <br />" +
                        "Error Message: " + ex;

                    dbContextTransaction.Rollback();

                    ViewBag.DefectGroup = new SelectList(db.DefectGroups, "DefectGroupId", "GroupName", "-- Select Group --");
                    ViewBag.VersionId = new SelectList(db.PhoneVersions, "VersionId", "Version");

                    return RedirectToAction("Create");
                }

                return RedirectToAction("Index");
            }
        }

        // GET: PossibleDefects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleDefect possibleDefect = db.PossibleDefects.Find(id);
            if (possibleDefect == null)
            {
                return HttpNotFound();
            }
            ViewBag.DefectGroup = new SelectList(db.DefectGroups, "DefectGroupId", "GroupName", possibleDefect.DefectGroupId);
            ViewBag.VersionId = new SelectList(db.PhoneVersions, "VersionId", "Version", possibleDefect.VersionId);
            return View(possibleDefect);
        }

        // POST: PossibleDefects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectGroupId,NewGroupName,PossibleDefectId,DefectName,DefectCost,VersionId")] int id, string DefectName, decimal DefectCost)
        {
            PossibleDefect possibleDefect = db.PossibleDefects.Find(id);

            try
            {
                if (ModelState.IsValid)
                {
                    possibleDefect.DefectName = DefectName;
                    possibleDefect.DefectCost = DefectCost;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error Encountered While Processing Request<br />" +
                    "Error Message: " + ex.Message;

                return View(id);
            }

            return View(possibleDefect);
        }

        // GET: PossibleDefects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleDefect possibleDefect = db.PossibleDefects.Find(id);
            if (possibleDefect == null)
            {
                return HttpNotFound();
            }
            return View(possibleDefect);
        }

        // POST: PossibleDefects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleDefect possibleDefect = db.PossibleDefects.Find(id);
            db.PossibleDefects.Remove(possibleDefect);
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
