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
    
    public partial class DefectTemplate
    {
        public int DefectTemplateId { get; set; }
        public string DefectName { get; set; }
        public decimal DefectCost { get; set; }
        public Nullable<int> DefectGroupId { get; set; }
    }
}
