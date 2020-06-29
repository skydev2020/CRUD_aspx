using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmSystemSetting
    {
        public int SettingId { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Value is required")]
        public string Value { get; set; }
    }
}