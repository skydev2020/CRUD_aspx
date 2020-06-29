using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmUser
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Permission")]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        public int Zip { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public Nullable<System.DateTime> LastLogin { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }

    }
}