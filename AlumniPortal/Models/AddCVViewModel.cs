﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class AddCVViewModel
    {
        public string alum_num { get; set; }
        public List<string> Experiences { get; set; }
        public List<string> Skills { get; set; }
        public List<string> EducAttainments { get; set; }
    }

}