using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EF_CRUD.Helpers;
using EF_CRUD.Models;

namespace EF_CRUD.Controllers
{
    public class SlideShowController : Controller
    {
        private CellableEntities db = new CellableEntities();

        private string imageLocation = WebConfigurationManager.AppSettings["ImageLocation"];
        private string webImageLocation = WebConfigurationManager.AppSettings["WebImageLocation"];

        // GET: SlideShows
        // test
        public ActionResult Index()
        {
            ViewBag.imageLocation = imageLocation;

            return View(db.SlideShows.ToList());
        }

        // GET: SlideShows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideShow slideShow = db.SlideShows.Find(id);
            if (slideShow == null)
            {
                return HttpNotFound();
            }
            return View(slideShow);
        }

        // GET: SlideShows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlideShows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideId,ImageName,Description")] SlideShow slideShow,
                                        HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                // Validate File Type Submitted
                var supportedFileTypes = new[] { "jpg", "bmp", "png" };
                var fileExt = System.IO.Path.GetExtension(imageFile.FileName).Substring(1);
                if (supportedFileTypes.Contains(fileExt))
                {
                    UploadFile uf = new UploadFile();
                    uf.UploadFileToBlobStorage(imageFile, "systemimages");

                    ViewBag.Message = "File uploaded successfully.";

                    // Insert Model
                    slideShow.ImageName = imageFile.FileName;
                }
            }

            if (slideShow.Description == null)
            {
                slideShow.Description = "NONE";
            }

            if (ModelState.IsValid)
            {
                db.SlideShows.Add(slideShow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideShow);
        }

        // GET: SlideShows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideShow slideShow = db.SlideShows.Find(id);
            if (slideShow == null)
            {
                return HttpNotFound();
            }
            return View(slideShow);
        }

        // POST: SlideShows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideId,ImageName,Description")] SlideShow slideShow,
                                        HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                string path = Server.MapPath("~/Images/");
                string webPath = webImageLocation;

                // Validate File Type Submitted
                var supportedFileTypes = new[] { "jpg", "bmp", "png" };
                var fileExt = System.IO.Path.GetExtension(imageFile.FileName).Substring(1);
                if (supportedFileTypes.Contains(fileExt))
                {
                    UploadFile uf = new UploadFile();
                    uf.UploadFileToBlobStorage(imageFile, "systemimages");
                    
                    ViewBag.Message = "File uploaded successfully.";

                    // Insert Model
                    slideShow.ImageName = imageFile.FileName;
                }
            }

            if (slideShow.Description == null)
            {
                slideShow.Description = "NONE";
            }

            if (ModelState.IsValid)
            {
                db.Entry(slideShow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideShow);
        }

        // GET: SlideShows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideShow slideShow = db.SlideShows.Find(id);
            if (slideShow == null)
            {
                return HttpNotFound();
            }
            return View(slideShow);
        }

        // POST: SlideShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlideShow slideShow = db.SlideShows.Find(id);
            db.SlideShows.Remove(slideShow);
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
