using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.Models
{
    public class ImageViewModel
    {
        public int VersionId { get; set; }

        [Required]
        public string Version { get; set; }

        [Description("Base Cost")]
        public Nullable<decimal> BaseCost { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Description("Image Name")]
        HttpPostedFileBase ImageName { get; set; }
    }
}