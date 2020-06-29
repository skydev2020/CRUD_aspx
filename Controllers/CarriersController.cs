using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EF_CRUD.Models;
using EF_CRUD.Helpers;
using System.Collections.Generic;
using EF_CRUD.View_Models;

namespace EF_CRUD.Controllers
{
    [Authorize]
    public class CarriersController : Controller
    {
        private CellableEntities db = new CellableEntities();

        private string imageLocation = WebConfigurationManager.AppSettings["ImageLocation"];
        private string webImageLocation = WebConfigurationManager.AppSettings["WebImageLocation"];

        // GET: Carriers
        // test
        public ActionResult Index(bool? active, FormCollection carrier_form)
        {
            List<Carrier> carriers = new List<Carrier>();

            if (active == null || active == true)
            {
                carriers = db.Carriers.AsNoTracking().Where(x => x.Active == true).OrderBy(x => x.Order).ToList();
            }
            else
            {
                carriers = db.Carriers.AsNoTracking().Where(x => x.Active == false).OrderBy(x => x.Order).ToList();
            }

            var model = (from c in carriers
                         select new vmCarrier()
                         {
                             Active = c.Active,
                             CarrierId = c.CarrierId,
                             CarrierName = c.CarrierName,
                             ImageName = c.ImageName,
                             Order = c.Order,
                             Value = c.Value
                         });

            ViewBag.CarrierOrder = new SelectList(carriers, "Order", "Order");
            ViewBag.imageLocation = imageLocation;

            return View(model.ToList());
        }

        public ActionResult ChangeOrder(FormCollection changeorder_form)
        {
            // Update the Order column
            foreach (var item in changeorder_form)
            {
                string formItem = item.ToString();

                if (formItem.Contains("_"))
                {
                    string[] carrierOrder = item.ToString().Split('_');

                    Carrier carrier = db.Carriers.Find(int.Parse(carrierOrder[0]));
                    carrier.Order = int.Parse(Request.Form[item.ToString()]);
                    db.Entry(carrier).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Carriers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrier carrier = db.Carriers.Find(id);
            if (carrier == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(carrier);
        }

        // GET: Carriers/Create
        public ActionResult Create()
        {
            vmCarrier carrier = new vmCarrier();

            return View(carrier);
        }

        // POST: Carriers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarrierId,CarrierName,ImageName,imageFile")] Carrier carrier, HttpPostedFileBase imageFile)
        {
            try
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

                        // Insert Model
                        carrier.ImageName = imageFile.FileName;
                    }
                }

                carrier.Active = true;
                db.Carriers.Add(carrier);
                db.SaveChanges();

                ViewBag.Message = "File uploaded successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error Encountered While Attempting to Process Request.<br />" +
                    "Error Message: " + ex.Message;

                ViewBag.imageLocation = imageLocation;

                return View(carrier);
            }
        }

        // GET: Carriers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Carrier carrier = db.Carriers.Find(id);

            var vmCarrier = new vmCarrier();

            vmCarrier.Active = carrier.Active;
            vmCarrier.CarrierId = carrier.CarrierId;
            vmCarrier.CarrierName = carrier.CarrierName;
            vmCarrier.ImageName = carrier.ImageName;
            vmCarrier.Order = carrier.Order;
            vmCarrier.Value = carrier.Value;

            if (carrier == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(vmCarrier);
        }

        // POST: Carriers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarrierId,CarrierName,ImageName,imageFile")] Carrier carrier, HttpPostedFileBase imageFile)
        {
            try
            {
                if (imageFile != null)
                {
                    string path = Server.MapPath("~/Images/");
                    string webPath = webImageLocation;

                    // Validate File Type Submitted
                    var supportedFileTypes = new[] { "jpg", "bmp", "png" };
                    var fileExt = System.IO.Path.GetExtension(imageFile.FileName).Substring(1);
                    imageFile.SaveAs(webPath + Path.GetFileName(imageFile.FileName));
                    if (supportedFileTypes.Contains(fileExt))
                    {
                        UploadFile uf = new UploadFile();
                        uf.UploadFileToBlobStorage(imageFile, "systemimages");

                        ViewBag.Message = "File uploaded successfully.";

                        // Insert Model
                        carrier.ImageName = imageFile.FileName;
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Entry(carrier).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error Encountered While Attempting to Process Request.<br />" +
                    "Error Message: " + ex.Message;

                ViewBag.imageLocation = imageLocation;

                return View(carrier);
            }
        }

        // GET: Carriers/Delete/5
        public ActionResult ToggleInactive(int? id)
        {
            Carrier carrier = db.Carriers.Find(id);

            if (carrier.Active == true)
            {
                carrier.Active = false;
            }
            else
            {
                carrier.Active = true;
            }

            db.SaveChanges();

            ViewBag.Active = carrier.Active;

            return RedirectToAction("Index");
        }

        // POST: Carriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrier carrier = db.Carriers.Find(id);
            db.Carriers.Remove(carrier);
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
