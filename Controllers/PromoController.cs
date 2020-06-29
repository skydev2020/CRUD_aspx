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
    public class PromoController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: Promo
        // test
        public ActionResult Index()
        {

            List<Promo> promos = new List<Promo>();


            var model = (from p in promos
                         select new vmPromos()
                         {
                             Discount = p.Discount,
                             DollarValue = p.DollarValue,
                             EndDate = p.EndDate,
                             PromoCode = p.PromoCode,
                             PromoId = p.PromoId,
                             PromoName = p.PromoName,
                             StartDate = p.StartDate
                         });

            return View(db.Promos.OrderBy(x => x.EndDate).ToList());
        }

        // GET: Promo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promos.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // GET: Promo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromoId,PromoCode,PromoName,StartDate,EndDate,Discount")] Promo promo)
        {
            decimal d = decimal.Parse(Request["Discount"].ToString());
            var discount = Request["Discount"];

            if (discount.Contains("."))
            {
                promo.Discount = decimal.Parse(discount);
            }
            else
            {
                promo.DollarValue = decimal.Parse(discount);
                promo.Discount = 0;
            }

            db.Promos.Add(promo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Promo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promos.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // POST: Promo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromoId,PromoCode,PromoName,StartDate,EndDate,Discount")] Promo promo)
        {
            decimal d = decimal.Parse(Request["Discount"].ToString());
            var discount = Request["Discount"];

            if (discount.Contains("."))
            {
                promo.Discount = decimal.Parse(discount);
            }
            else
            {
                promo.DollarValue = decimal.Parse(discount);
                promo.Discount = 0;
            }

            db.Entry(promo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Promo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promos.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // POST: Promo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promo promo = db.Promos.Find(id);
            db.Promos.Remove(promo);
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
