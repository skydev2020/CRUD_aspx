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
    public class TestimonialsController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: Testimonials
        // test
        public ActionResult Index()
        {
            List<vmTestimonial> orderDetailsVMlist = new List<vmTestimonial>();

            return View(db.Testimonials.ToList());
        }

        // GET: Testimonials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // GET: Testimonials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestimonialId,Text,Rating,UserId,CreateDate")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                db.Testimonials.Add(testimonial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        public ActionResult Publish(int id)
        {
            Testimonial testimonial = db.Testimonials.Find(id);
            if(testimonial.Published == true)
            {
                testimonial.Published = null;
            }
            else
            {
                testimonial.Published = true;
            }
            db.Entry(testimonial).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestimonialId,Text,Rating,UserId,CreateDate")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testimonial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Testimonial testimonial = db.Testimonials.Find(id);
            db.Testimonials.Remove(testimonial);
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
