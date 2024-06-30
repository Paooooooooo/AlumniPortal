using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class ManageCVViewModel
    {
        public alum_tbl AlumniInfo { get; set; }
        public List<cv_tbl> CVs { get; set; }
        public bool HasCVEntries { get; set; }
    }
}