using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmStorageCapacity
    {
        public int StorageCapacityId { get; set; }

        [Required(ErrorMessage = "Storage Capcity is required")]
        [Display(Name = "Storage Capacity")]
        public int StorageCapacity1 { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

    }
}