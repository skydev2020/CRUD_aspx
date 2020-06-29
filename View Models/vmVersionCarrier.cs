using EF_CRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmVersionCarrier
    {
        public int VersionCarrierId { get; set; }

        [Required(ErrorMessage = "Version is required")]
        [Display(Name = "Version ID")]
        public int VersionId { get; set; }

        [Required(ErrorMessage = "Carrier is required")]
        [Display(Name = "Carrier ID")]
        public int CarrierId { get; set; }
        public decimal Value { get; set; }

        public virtual Carrier Carrier { get; set; }
        public virtual PhoneVersion PhoneVersion { get; set; }
    }
}