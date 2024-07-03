using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class JobPostIndexModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public string CompanyName { get; set; }
    }
}