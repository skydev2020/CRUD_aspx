//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF_CRUD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Testimonial
    {
        public int TestimonialId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<bool> Published { get; set; }
    }
}
