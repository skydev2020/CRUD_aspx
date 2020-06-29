using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmTestimonial
    {
        public int TestimonialId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<bool> Published { get; set; }
    }
}