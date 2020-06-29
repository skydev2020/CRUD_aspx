using EF_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmUserPhone
    {
        public int UserPhoneId { get; set; }
        public int UserId { get; set; }
        public int PhoneId { get; set; }
        public int CarrierId { get; set; }
        public int VersionId { get; set; }
        public System.DateTime CreateDate { get; set; }

        public virtual Carrier Carrier { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual PhoneVersion PhoneVersion { get; set; }
        public virtual User User { get; set; }
    }
}