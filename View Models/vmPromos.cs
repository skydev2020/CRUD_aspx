using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmPromos
    {
        public int PromoId { get; set; }

        [Required(ErrorMessage = "Promo Code is required")]
        [Display(Name = "Promo Code")]
        public string PromoCode { get; set; }

        [Required(ErrorMessage = "Promo Name is required")]
        [Display(Name = "Promo Name")]
        public string PromoName { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Dollar Value is required")]
        [Display(Name = "Dollar Value")]
        public Nullable<decimal> DollarValue { get; set; }

    }
}