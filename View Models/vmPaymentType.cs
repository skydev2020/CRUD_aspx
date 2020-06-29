using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmPaymentType
    {
        public int PaymentTypeId { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType1 { get; set; }
    }
}