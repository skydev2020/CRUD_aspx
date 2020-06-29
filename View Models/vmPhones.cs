using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmPhones
    {
        public int PhoneId { get; set; }
        public string Brand { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }

    }
}