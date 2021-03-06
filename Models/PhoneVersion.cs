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
    
    public partial class PhoneVersion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhoneVersion()
        {
            this.PossibleDefects = new HashSet<PossibleDefect>();
            this.UserPhones = new HashSet<UserPhone>();
            this.VersionCapacities = new HashSet<VersionCapacity>();
            this.VersionCarriers = new HashSet<VersionCarrier>();
        }
    
        public int VersionId { get; set; }
        public string Version { get; set; }
        public Nullable<decimal> BaseCost { get; set; }
        public string ImageName { get; set; }
        public Nullable<int> PhoneId { get; set; }
        public Nullable<int> StorageCapacityId { get; set; }
        public Nullable<int> Views { get; set; }
        public Nullable<int> Purchases { get; set; }
        public Nullable<bool> Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossibleDefect> PossibleDefects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPhone> UserPhones { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual StorageCapacity StorageCapacity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VersionCapacity> VersionCapacities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VersionCarrier> VersionCarriers { get; set; }
    }
}
