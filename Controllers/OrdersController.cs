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
    public class OrdersController : Controller
    {
        private CellableEntities db = new CellableEntities();

        // GET: Orders
        // test
        public ActionResult Index(int? orderId, int? orderStatusId, int? filterStatusId, string search = null)
        {

            List<Order> orders = new List<Order>();


            var model = (from o in orders
                         select new vmOrder()
                         {
                            Amount = o.Amount,
                            CreateBy = o.CreateBy,
                            CreateDate = o.CreateDate,
                            MailingLabel = o.MailingLabel,
                            OrderID = o.OrderID,
                            OrderStatu = o.OrderStatu,
                            OrderStatusId =o.OrderStatusId,
                            PaymentType = o.PaymentType,
                            PaymentTypeId = o.PaymentTypeId,
                            PaymentUserName = o.PaymentUserName,
                            Promo = o.Promo,
                            PromoId = o.PromoId,
                            User = o.User,
                            UserId = o.UserId,
                            UserPhoneId = o.UserPhoneId,
                            USPSTrackingId = o.USPSTrackingId
                         });
            // Update User Order Status
            if (orderId != null && orderStatusId != null)
            {
                Order order = db.Orders.Find(orderId);
                order.OrderStatusId = orderStatusId;
                db.SaveChanges();
            }

            IList<OrderStatu> orderStatus = db.OrderStatus.ToList();
            ViewBag.filterStatusId = new SelectList(orderStatus, "OrderStatusId", "StatusType", filterStatusId);
            ViewBag.Search = search;
            ViewBag.OrderStatus = orderStatus;

            List<vmOrderDetails> orderDetailsVMlist = new List<vmOrderDetails>();

            try
            {

                // Refer to DB/GetOrderDetails.sql
                var results = (from o in db.Orders.DefaultIfEmpty()
                               join up in db.UserPhones on o.UserId equals up.UserId into userPhoneGrp
                               from up in userPhoneGrp.DefaultIfEmpty()
                               join u in db.Users on up.UserId equals u.UserId into userGrp
                               from u in userGrp.DefaultIfEmpty()
                               join pv in db.PhoneVersions on up.VersionId equals pv.VersionId into phoneVersionsGrp
                               from pv in phoneVersionsGrp.DefaultIfEmpty()
                               join os in db.OrderStatus on o.OrderStatusId equals os.OrderStatusId into orderStatusGrp
                               from os in orderStatusGrp.DefaultIfEmpty()
                               join pt in db.PaymentTypes on o.PaymentTypeId equals pt.PaymentTypeId into paymentTypesGrp
                               from pt in paymentTypesGrp.DefaultIfEmpty()
                               join p in db.Promos on o.PromoId equals p.PromoId into promosGrp
                               from p in promosGrp.DefaultIfEmpty()
                               join ph in db.Phones on pv.PhoneId equals ph.PhoneId into phonesGrp
                               from ph in phonesGrp.DefaultIfEmpty()
                               where o.UserPhoneId == up.UserPhoneId

                               orderby o.CreateDate

                               select new vmOrderDetails()
                               {
                                   OrderId = o.OrderID,
                                   Amount = o.Amount,
                                   Brand = ph.Brand,
                                   Version = pv.Version,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   PhoneNumber = u.PhoneNumber,
                                   Address = u.Address,
                                   Address2 = u.Address2,
                                   City = u.City,
                                   State = u.State,
                                   Zip = u.Zip,
                                   StatusType = os.StatusType,
                                   OrderStatusId = o.OrderStatusId,
                                   PromoCode = p.PromoCode,
                                   PromoName = p.PromoName,
                                   Discount = p.Discount,
                                   PaymentType = pt.PaymentType1,
                                   PaymentUserName = o.PaymentUserName,
                                   CreateDate = o.CreateDate
                               }).ToList();

                // Search Filter Starts Here  -->>
                if (search != null)
                {
                    if (search != "" && filterStatusId == null)
                    {
                        // If Search is a Number...
                        int result = 0;
                        int.TryParse(search, out result);

                        string phoneNumber = "";
                        phoneNumber = search.Replace("(", "");
                        phoneNumber = search.Replace(")", "");
                        phoneNumber = search.Replace("-", "");
                        phoneNumber = search.Replace("_", "");

                        results = results.Where(x => x.OrderId == result
                                                || x.FirstName.ToLower().Contains(search.ToLower())
                                                || x.LastName.ToLower().Contains(search.ToLower())
                                                || (x.FirstName.ToLower() + " " + x.LastName.ToLower()).Contains(search.ToLower())
                                                || x.PhoneNumber == phoneNumber
                                                || x.Email.ToLower() == search.ToLower()).ToList();
                    }

                    if (filterStatusId != null && search == "")
                    {
                        results = results.Where(x => x.OrderStatusId == filterStatusId).ToList();
                    }

                    if (search != "" && filterStatusId != null)
                    {
                        // If Search is a Number...
                        int searchOrderId = 0;
                        int.TryParse(search, out searchOrderId);

                        string phoneNumber = "";
                        phoneNumber = search.Replace("(", "");
                        phoneNumber = search.Replace(")", "");
                        phoneNumber = search.Replace("-", "");
                        phoneNumber = search.Replace("_", "");

                        results = results.Where(x => x.OrderId == searchOrderId
                                                || x.FirstName.ToLower().Contains(search.ToLower())
                                                || x.LastName.ToLower().Contains(search.ToLower())
                                                || (x.FirstName.ToLower() + " " + x.LastName.ToLower()).Contains(search.ToLower())
                                                || x.PhoneNumber == phoneNumber
                                                || x.Email.ToLower() == search.ToLower()).ToList();

                        results = results.Where(x => x.OrderStatusId == filterStatusId).ToList();
                    }
                }

                foreach (var item in results)
                {
                    vmOrderDetails vmDetails = new vmOrderDetails();

                    vmDetails.OrderId = item.OrderId;
                    vmDetails.Amount = item.Amount;
                    vmDetails.Brand = item.Brand;
                    vmDetails.Version = item.Version;
                    vmDetails.FirstName = item.FirstName;
                    vmDetails.LastName = item.LastName;
                    vmDetails.Email = item.Email;
                    vmDetails.PhoneNumber = item.PhoneNumber;
                    vmDetails.Address = item.Address;
                    vmDetails.Address2 = item.Address2;
                    vmDetails.City = item.City;
                    vmDetails.State = item.State;
                    vmDetails.Zip = item.Zip;
                    vmDetails.StatusType = item.StatusType;
                    vmDetails.OrderStatusId = item.OrderStatusId;
                    vmDetails.PromoCode = item.PromoCode;
                    vmDetails.PromoName = item.PromoName;
                    vmDetails.Discount = item.Discount;
                    vmDetails.PaymentType = item.PaymentType;
                    vmDetails.PaymentUserName = item.PaymentUserName;
                    vmDetails.CreateDate = item.CreateDate;
                    orderDetailsVMlist.Add(vmDetails);
                }
            }
            catch (Exception ex)
            {

                return View(orderDetailsVMlist);
            }

            return View(orderDetailsVMlist);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.OrderStatusId = new SelectList(db.OrderStatus, "OrderStatusId", "StatusType");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1");
            ViewBag.PromoId = new SelectList(db.Promos, "PromoId", "PromoCode");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Amount,UserId,OrderStatusId,CreateDate,CreateBy,PaymentTypeId,PromoId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", order.UserId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatus, "OrderStatusId", "StatusType", order.OrderStatusId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1", order.PaymentTypeId);
            ViewBag.PromoId = new SelectList(db.Promos, "PromoId", "PromoCode", order.PromoId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", order.UserId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatus, "OrderStatusId", "StatusType", order.OrderStatusId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1", order.PaymentTypeId);
            ViewBag.PromoId = new SelectList(db.Promos, "PromoId", "PromoCode", order.PromoId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Amount,UserId,OrderStatusId,CreateDate,CreateBy,PaymentTypeId,PromoId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", order.UserId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatus, "OrderStatusId", "StatusType", order.OrderStatusId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1", order.PaymentTypeId);
            ViewBag.PromoId = new SelectList(db.Promos, "PromoId", "PromoCode", order.PromoId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public PartialViewResult Load()
        {
            return PartialView("OrderDetails");
        }
    }
}
