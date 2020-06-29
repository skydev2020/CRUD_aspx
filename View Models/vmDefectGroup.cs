using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmDefectGroup
    {
        [Display(Name = "Defect Group")]
        public int DefectGroupId { get; set; }

        [Required(ErrorMessage = "Group Name is required")]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        public Nullable<int> DisplayOrder { get; set; }

        public string Info { get; set; }

    }
}