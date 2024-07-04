using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class ChangePasswordCompany
    {
        public int CompId { get; set; }
        public string CurrentCompPassword { get; set; }
        public string NewCompPassword { get; set; }
        public string ConfirmCompPassword { get; set; }
    }
}