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
    public class SystemSettingsController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: SystemSettings
        // test
        public ActionResult Index()
        {

            List<SystemSetting> systems = new List<SystemSetting>();


            var model = (from s in systems 
                         select new vmSystemSetting()
                         {
                             Name = s.Name,
                             SettingId = s.SettingId,
                             Value = s.Value
                         });
            return View(db.SystemSettings.ToList().OrderBy(x => x.Name));
        }

        // GET: SystemSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            if (systemSetting == null)
            {
                return HttpNotFound();
            }
            return View(systemSetting);
        }

        // GET: SystemSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SettingId,Name,Value")] SystemSetting systemSetting)
        {
            if (ModelState.IsValid)
            {
                db.SystemSettings.Add(systemSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemSetting);
        }

        // GET: SystemSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            if (systemSetting == null)
            {
                return HttpNotFound();
            }
            return View(systemSetting);
        }

        // POST: SystemSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SettingId,Name,Value")] SystemSetting systemSetting)
        {

            try
            {
                db.Entry(systemSetting).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

        // GET: SystemSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            if (systemSetting == null)
            {
                return HttpNotFound();
            }
            return View(systemSetting);
        }

        // POST: SystemSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            db.SystemSettings.Remove(systemSetting);
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
