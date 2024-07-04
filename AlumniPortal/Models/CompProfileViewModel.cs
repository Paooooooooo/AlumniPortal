using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class CompProfileViewModel
    {
        public int CompId { get; set; }
        public string CompName { get; set; }
        public string CompConNum { get; set; }
        public string CompConName { get; set; }
        public string CompEmail { get; set; }
        public string CompAddress { get; set; }
    }
}