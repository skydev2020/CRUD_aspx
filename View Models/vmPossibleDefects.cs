using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmPossibleDefects
    {
        public int PossibleDefectId { get; set; }

        [Required(ErrorMessage = "Defect Name is required")]
        [Display(Name = "Defect Name")]
        public string DefectName { get; set; }

        [Required(ErrorMessage = "Defect Cost is required")]
        [Display(Name = "Defect Cost Name")]
        public decimal DefectCost { get; set; }
        public int VersionId { get; set; }
        public Nullable<int> DefectGroupId { get; set; }
        public string GroupImage { get; set; }

    }
}