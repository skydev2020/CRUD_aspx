using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EF_CRUD.Helpers;
using EF_CRUD.Models;
using EF_CRUD.View_Models;

namespace EF_CRUD.Controllers
{
    [Authorize]
    public class PhonesController : Controller
    {
        private CellableEntities db = new CellableEntities();

        private string imageLocation = WebConfigurationManager.AppSettings["ImageLocation"];
        private string webImageLocation = WebConfigurationManager.AppSettings["WebImageLocation"];

        // GET: Phones
        public ActionResult Index()
        {

            List<Phone> phone = new List<Phone>();


            var model = (from c in phone
                         select new vmPhones()
                         {
                           Brand = c.Brand,
                           ImageName = c.ImageName,
                           PhoneId = c.PhoneId,
                         });

            ViewBag.imageLocation = imageLocation;

            return View(db.Phones.ToList());
        }

        // GET: Phones/Details/5
        // test
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhoneId,Brand,ImageName,imageFile")] Phone phone, HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                string adminPath = Server.MapPath("~/Images/");
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
                    phone.ImageName = imageFile.FileName;
                }
            }

            ViewBag.imageLocation = imageLocation;

            db.Phones.Add(phone);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhoneId,Brand,ImageName,imageFile")] Phone phone, HttpPostedFileBase imageFile)
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
                    phone.ImageName = imageFile.FileName;
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.imageLocation = imageLocation;

            return View(phone);
        }

        // GET: Phones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
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
