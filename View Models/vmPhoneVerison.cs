using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmPhoneVerison
    {
        public int VersionId { get; set; }

        [Required(ErrorMessage = "Version is required")]
        public string Version { get; set; }

        [Display(Name = "Base Cost")]
        public Nullable<decimal> BaseCost { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public Nullable<int> PhoneId { get; set; }

        [Display(Name = "Storage Capacity")]
        public Nullable<int> StorageCapacityId { get; set; }
        public Nullable<int> Views { get; set; }
        public Nullable<int> Purchases { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}