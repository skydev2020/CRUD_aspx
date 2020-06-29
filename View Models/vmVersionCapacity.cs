using EF_CRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmVersionCapacity
    {
        public int VersionCapacityId { get; set; }

        [Required(ErrorMessage = "Version is required")]
        [Display(Name = "Version ID")]
        public int VersionId { get; set; }

        [Required(ErrorMessage = "Storage Capacity is required")]
        [Display(Name = "Storage Capacity ID")]
        public int StorageCapacityId { get; set; }
        public Nullable<decimal> Value { get; set; }

        public virtual PhoneVersion PhoneVersion { get; set; }
        public virtual StorageCapacity StorageCapacity { get; set; }
    }
}