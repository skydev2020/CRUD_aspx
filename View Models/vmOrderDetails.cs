using System;
using System.ComponentModel.DataAnnotations;

namespace EF_CRUD.View_Models
{
    public class vmOrderDetails
    {
        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public string Brand { get; set; }

        public string Version { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Display(Name = "Status Type")]
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int Zip { get; set; }
               
        [Display(Name = "Status Type")]
        public string StatusType { get; set; }

        public Nullable<int> OrderStatusId { get; set; }

        [Display(Name = "Promo Code")]
        public string PromoCode { get; set; }

        [Display(Name = "Promo Name")]
        public string PromoName { get; set; }

        public decimal? Discount { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Payment User Name")]
        public string PaymentUserName { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
    }
}