using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmOrderStatu
    {
        public int OrderStatusId { get; set; }

        [Display(Name = "Ststus Type")]
        public string StatusType { get; set; }

    }
}