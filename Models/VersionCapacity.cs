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
    
    public partial class VersionCapacity
    {
        public int VersionCapacityId { get; set; }
        public int VersionId { get; set; }
        public int StorageCapacityId { get; set; }
        public Nullable<decimal> Value { get; set; }
    
        public virtual PhoneVersion PhoneVersion { get; set; }
        public virtual StorageCapacity StorageCapacity { get; set; }
    }
}
