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
    
    public partial class VersionCarrier
    {
        public int VersionCarrierId { get; set; }
        public int VersionId { get; set; }
        public int CarrierId { get; set; }
        public decimal Value { get; set; }
    
        public virtual Carrier Carrier { get; set; }
        public virtual PhoneVersion PhoneVersion { get; set; }
    }
}
