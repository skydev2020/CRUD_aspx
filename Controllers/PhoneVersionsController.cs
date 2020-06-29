using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EF_CRUD.Helpers;
using EF_CRUD.Models;
using EF_CRUD.View_Models;

namespace EF_CRUD.Controllers
{
    [Authorize]
    public class PhoneVersionsController : Controller
    {
        private CellableEntities db = new CellableEntities();

        private string imageLocation = WebConfigurationManager.AppSettings["PhoneImageLocation"];
        private string webImageLocation = WebConfigurationManager.AppSettings["WebImageLocation"];
        private string storageConnectionString = WebConfigurationManager.AppSettings["StorageConnectionString"];


        // GET: PhoneVersions
        // test
        public ActionResult Index(string searchString = null, bool active = true)
        {

            List<PhoneVersion> phoneVersion = new List<PhoneVersion>();


            var model = (from pv in phoneVersion
                         select new vmPhoneVerison()
                         {
                            BaseCost = pv.BaseCost,
                            Active = pv.Active,
                            ImageName = pv.ImageName,
                            PhoneId = pv.PhoneId,
                            Purchases = pv.Purchases,
                            StorageCapacityId = pv.StorageCapacityId,
                            Version = pv.Version,
                            VersionId = pv.VersionId,
                            Views = pv.Views
                         });

            var phoneVersions = Search(active, searchString);

            ViewBag.imageLocation = imageLocation;

            return View(phoneVersions);
        }

        public IList<PhoneVersion> Search(bool active, string searchString = null)
        {
            // Keep Any Previously Submitted Values
            ViewBag.Active = active;
            ViewBag.SearchString = searchString;

            // Initialize PhoneVersions Variable
            IList<PhoneVersion> phoneVersions = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                phoneVersions = db.PhoneVersions.ToList().Where(x => x.Phone.Brand.ToLower().Contains(searchString.ToLower())
                                                                    || x.Version.ToLower().Contains(searchString.ToLower())
                                                                    && x.Active == active).ToList();

                return phoneVersions;
            }

            // Reset Any Previously Submitted Values
            ViewBag.Active = string.Empty;
            ViewBag.SearchString = string.Empty;

            // No Filter Set, Return All Rows
            phoneVersions = db.PhoneVersions.Where(x => x.Active == active).ToList();

            return phoneVersions;
        }

        // GET: PhoneVersions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneVersion phoneVersion = db.PhoneVersions.Find(id);
            if (phoneVersion == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(phoneVersion);
        }

        // GET: PhoneVersions/Create
        public ActionResult Create()
        {
            // Get Dropdown Filter Option Values For Brands & Carriers
            ViewBag.PhoneBrands = db.Phones.ToList();

            // Get Dropdown Filter Option Values For Carriers
            ViewBag.Carriers = db.Carriers.ToList();

            // Get Dropdown Filter Option Values For Storage Capacity
            ViewBag.StorageCapacities = db.StorageCapacities.ToList();

            ViewBag.imageLocation = webImageLocation;
            ViewBag.ConnectionString = storageConnectionString;

            return View();
        }

        // POST: PhoneVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "carrierId,VersionId,Version,BaseCost,ImageName")]
                                        PhoneVersion phoneVersion,
                                        int brandId,
                                        HttpPostedFileBase imageFile,
                                        FormCollection form)
        {
            if (imageFile != null)
            {
                // Validate File Type Submitted
                var supportedFileTypes = new[] { "jpg", "JPG", "png", "PNG", "gif", "GIF" };
                var fileExt = System.IO.Path.GetExtension(imageFile.FileName).Substring(1);
                if (supportedFileTypes.Contains(fileExt))
                {
                    UploadFile uf = new UploadFile();
                    uf.UploadFileToBlobStorage(imageFile, "phoneimages");

                    ViewBag.Message = "File uploaded successfully.";

                    // Insert Model
                    phoneVersion.ImageName = imageFile.FileName;
                }
            }

            // Insert Phone Brand into Model
            phoneVersion.PhoneId = brandId;
            phoneVersion.Active = true;

            // Save New Phone Version
            db.PhoneVersions.Add(phoneVersion);
            db.SaveChanges();

            try
            {
                // Create Defect Questions (Template)
                SqlParameter param1 = new SqlParameter("@NewVersionID", phoneVersion.VersionId);
                db.Database.ExecuteSqlCommand("CreateVersionDefectTemplate @NewVersionID",
                                              param1);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex;
            }

            try
            {
                foreach (var item in form)
                {
                    string formItem = item.ToString();

                    if (formItem.Contains("Carrier_"))
                    {
                        var carrierId = item.ToString().Replace("Carrier_", "");
                        var value = Request[formItem].ToString();

                        SaveVersionCarrier(phoneVersion.VersionId, int.Parse(carrierId.ToString()), int.Parse(value));
                    }
                }

                // Insert Storage Capacities
                foreach (var item in form)
                {
                    string formItem = item.ToString();

                    if (formItem.Contains("Capacity_") && Request[formItem] != "")
                    {
                        var id = Request[formItem];
                        id.Replace("Capacity_", "");
                        VersionCapacity versionCapacity = new VersionCapacity();
                        versionCapacity.StorageCapacityId = int.Parse(Request[formItem]);
                        versionCapacity.VersionId = phoneVersion.VersionId;
                        versionCapacity.Value = decimal.Parse(Request["Value_" + id].ToString());
                        db.VersionCapacities.Add(versionCapacity);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex;
            }

            return RedirectToAction("Index");
        }

        public void SaveVersionCarrier(int versionId, int carrierId, int value)
        {
            VersionCarrier versionCarrier = new VersionCarrier();
            versionCarrier.VersionId = versionId;
            versionCarrier.CarrierId = carrierId;
            versionCarrier.Value = decimal.Parse(value.ToString());
            db.VersionCarriers.Add(versionCarrier);
            db.SaveChanges();
        }

        // GET: PhoneVersions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhoneVersion phoneVersion = db.PhoneVersions.Find(id);
            if (phoneVersion == null)
            {
                return HttpNotFound();
            }

            ViewBag.PhoneId = new SelectList(db.Phones, "PhoneId", "Brand", phoneVersion.PhoneId);

            // Get Dropdown Filter Option Values For Storage Capacity
            ViewBag.StorageCapacities = db.StorageCapacities.ToList();

            ViewBag.VersionStorageCapacities = db.VersionCapacities.Where(x => x.VersionId == id).ToList();

            // Get List of Carriers
            ViewBag.Carriers = db.Carriers.ToList();

            // Get VersionCarrier Values
            ViewBag.VersionCarriers = db.VersionCarriers.Where(x => x.PhoneVersion.VersionId == phoneVersion.VersionId).ToList();

            ViewBag.imageLocation = imageLocation;

            return View(phoneVersion);
        }

        // POST: PhoneVersions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhoneVersion phoneVersion,
                                    int PhoneId,
                                    string Version,
                                    decimal baseCost,
                                    HttpPostedFileBase imageFile,
                                    FormCollection form)
        {
            // Check for Newly Uploaded Image
            if (imageFile != null)
            {
                string path = Server.MapPath("~/Images/Phones/");
                string webPath = webImageLocation;

                // Validate File Type Submitted
                var supportedFileTypes = new[] { "jpg", "bmp", "png" };
                var fileExt = System.IO.Path.GetExtension(imageFile.FileName).Substring(1);
                if (supportedFileTypes.Contains(fileExt))
                {
                    UploadFile uf = new UploadFile();
                    uf.UploadFileToBlobStorage(imageFile, "phoneimages");

                    ViewBag.Message = "File uploaded successfully.";

                    // Update Model
                    phoneVersion.ImageName = imageFile.FileName;
                }
            }

            // Update Model
            phoneVersion.PhoneId = PhoneId;
            phoneVersion.Version = Version;
            phoneVersion.BaseCost = baseCost;
            phoneVersion.Active = true;

            // Update Database
            db.Entry(phoneVersion).State = EntityState.Modified;
            db.SaveChanges();

            // Remove Previously Saved Version Carriers
            var removeVersionCarrier = db.VersionCarriers.Where(x => x.VersionId == phoneVersion.VersionId).ToList();
            if (removeVersionCarrier != null)
            {
                db.VersionCarriers.RemoveRange(removeVersionCarrier);
                db.SaveChanges();
            }

            foreach (var item in form)
            {
                string formItem = item.ToString();

                if (formItem.Contains("Carrier_"))
                {
                    var carrierId = item.ToString().Replace("Carrier_", "");
                    var value = Request[formItem].ToString();

                    SaveVersionCarrier(phoneVersion.VersionId, int.Parse(carrierId.ToString()), int.Parse(value));
                }
            }

            // Remove Previously Saved Storage Capacities
            var removeVersionCapacity = db.VersionCapacities.Where(x => x.VersionId == phoneVersion.VersionId).ToList();
            if (removeVersionCapacity != null)
            {
                db.VersionCapacities.RemoveRange(removeVersionCapacity);
                db.SaveChanges();
            }

            // Insert Storage Capacities
            foreach (var item in form)
            {
                string formItem = item.ToString();
                if (formItem.Contains("Capacity_") && Request[formItem] != "")
                {
                    var id = Request[formItem];
                    id.Replace("Capacity_", "");
                    VersionCapacity versionCapacity = new VersionCapacity();
                    versionCapacity.StorageCapacityId = int.Parse(Request[formItem]);
                    versionCapacity.VersionId = phoneVersion.VersionId;
                    versionCapacity.Value = decimal.Parse(Request["Value_" + id].ToString());
                    db.VersionCapacities.Add(versionCapacity);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult ToggleActive(int id)
        {
            PhoneVersion phoneVersion = db.PhoneVersions.Find(id);

            if (phoneVersion.Active == true)
            {
                phoneVersion.Active = false;
            }
            else
            {
                phoneVersion.Active = true;
            }

            db.SaveChanges();

            ViewBag.Active = phoneVersion.Active;

            return RedirectToAction("Index");
        }

        // GET: PhoneVersions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneVersion phoneVersion = db.PhoneVersions.Find(id);
            if (phoneVersion == null)
            {
                return HttpNotFound();
            }

            ViewBag.imageLocation = imageLocation;

            return View(phoneVersion);
        }

        // POST: PhoneVersions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    IList<VersionCapacity> versionCapacity = db.VersionCapacities.Where(x => x.VersionId == id).ToList();
        //    db.VersionCapacities.RemoveRange(versionCapacity);
        //    db.SaveChanges();

        //    PhoneVersion phoneVersion = db.PhoneVersions.Find(id);
        //    db.PhoneVersions.Remove(phoneVersion);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

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
