using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmPermission
    {
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "Permission Type is required")]
        [Display(Name = "Permission Type")]
        public string PermissionType { get; set; }
    }
}