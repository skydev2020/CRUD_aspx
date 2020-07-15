using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EF_CRUD.Models;
using System.Web.Security;
using System.Collections.Generic;
using System;
using EF_CRUD.View_Models;

namespace EF_CRUD.Controllers
{
    public class UsersController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // User Login
        // test
        public ActionResult Login()
        {
            return View();
        }

        // User Logout
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        // Dashboard
        [Authorize]
        public ActionResult Dashboard()
        {
            // Get New Orders
            IList<Order> orders = db.Orders.ToList().Where(x => x.OrderStatusId == 1).ToList();
            ViewBag.NewOrders = orders;
            ViewBag.OrderCount = orders.Count();

            // Get Most Viewed Versions
            IList<PhoneVersion> viewedVersions = db.PhoneVersions.ToList().Where(x=> x.Active == true).OrderByDescending(x => x.Views).ToList();
            ViewBag.MostViewed = viewedVersions;

            // Get Most Purchased Versions
            IList<PhoneVersion> purchasedVersions = db.PhoneVersions.ToList().OrderByDescending(x => x.Purchases).ToList();
            ViewBag.MostPurchased = purchasedVersions;

            // Get Promotions
            IList<Promo> promos = db.Promos.ToList().OrderBy(x => x.EndDate).ToList();
            ViewBag.Promotions = promos;

            // Get PaymentTypes
            IList<PaymentType> paymentTypes = db.PaymentTypes.ToList();
            ViewBag.PaymentTypes = paymentTypes;

            return View();
        }

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {

            List<User> user = new List<User>();


            var model = (from u in user
                         select new vmUser()
                         {
                            Address = u.Address,
                            Address2 = u.Address2,
                            City = u.City,
                            CreateDate = u.CreateDate,
                            CreatedBy = u.CreatedBy,
                            Email = u.Email,
                            FirstName = u.FirstName,
                            LastLogin = u.LastLogin,
                            LastName = u.LastName,
                            Password = u.Password,
                            PermissionId = u.PermissionId,
                            PhoneNumber = u.PhoneNumber,
                            State = u.State,
                            UserId = u.UserId,
                            UserName = u.UserName,
                            Zip = u.Zip
                         });





            var users = db.Users.Include(u => u.Permission);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionType");
            ViewBag.State = new SelectList(db.States, "StateAbbrv", "StateName");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Password,PermissionId,FirstName,LastName,Email,Address,Address2,City,State,Zip,PhoneNumber,LastLogin,CreateDate,CreatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreateDate = DateTime.Now;
                user.CreatedBy = "System";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionType", user.PermissionId);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionType", user.PermissionId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Password,PermissionId,FirstName,LastName,Email,Address,Address2,City,State,Zip,PhoneNumber,LastLogin,CreateDate,CreatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "PermissionType", user.PermissionId);
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
