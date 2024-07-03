using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class JobPostDetailsModel
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public string JobSpecifications { get; set; }
        public string JobSalary { get; set; }
        public string JobType { get; set; }
        public string JobTarget { get; set; }
        public string CompanyContactName { get; set; }
        public string CompanyContactNumber { get; set; }
        public string CompanyEmail { get; set; }
    }
}