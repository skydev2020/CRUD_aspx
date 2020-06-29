using EF_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_CRUD.View_Models
{
    public class vmUserAnswer
    {
        public int AnswerId { get; set; }
        public bool Answer { get; set; }
        public int PossibleDefectId { get; set; }
        public int UserPhoneId { get; set; }

        public virtual PossibleDefect PossibleDefect { get; set; }
        public virtual UserPhone UserPhone { get; set; }
    }
}