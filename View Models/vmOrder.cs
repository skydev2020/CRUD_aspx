using EF_CRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmOrder
    {
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public Nullable<int> OrderStatusId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public Nullable<int> PromoId { get; set; }
        public Nullable<int> UserPhoneId { get; set; }

        [Display(Name = "Payment User Name")]
        public string PaymentUserName { get; set; }

        [Display(Name = "USPS Tracking ID")]
        public string USPSTrackingId { get; set; }

        [Display(Name = "Mailing Label")]
        public string MailingLabel { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Order Status")]
        public virtual OrderStatu OrderStatu { get; set; }

        [Display(Name = "Payment Type")]
        public virtual PaymentType PaymentType { get; set; }
        public virtual Promo Promo { get; set; }
    }
}