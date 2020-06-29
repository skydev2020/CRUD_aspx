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
    
    public partial class PossibleDefect
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PossibleDefect()
        {
            this.UserAnswers = new HashSet<UserAnswer>();
        }
    
        public int PossibleDefectId { get; set; }
        public string DefectName { get; set; }
        public decimal DefectCost { get; set; }
        public int VersionId { get; set; }
        public Nullable<int> DefectGroupId { get; set; }
        public string GroupImage { get; set; }
    
        public virtual PhoneVersion PhoneVersion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public virtual DefectGroup DefectGroup { get; set; }
    }
}
