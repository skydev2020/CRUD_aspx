using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmDefectTemplate
    {
        public int DefectTemplateId { get; set; }

        [Required(ErrorMessage = "Defect Name is required")]
        [Display(Name = "Defect Name")]
        public string DefectName { get; set; }

        [Required(ErrorMessage = "Defect Cost is required")]
        [Display(Name = "Defect Cost")]
        public decimal DefectCost { get; set; }

        [Display(Name = "Defect Group")]
        public Nullable<int> DefectGroupId { get; set; }
    }
}