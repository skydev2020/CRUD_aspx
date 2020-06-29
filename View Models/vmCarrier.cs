using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmCarrier
    {
        public int CarrierId { get; set; }

        [Required(ErrorMessage = "Carrier Name is required")]
        [Display(Name = "Carrier Name")]
        public string CarrierName { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Order { get; set; }

    }
}