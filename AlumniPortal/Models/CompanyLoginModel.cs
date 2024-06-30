using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class CompanyLoginModel
    {
        public int comp_id {  get; set; }
        public string comp_email { get; set; }
        public string comp_password { get; set; }
    }
}