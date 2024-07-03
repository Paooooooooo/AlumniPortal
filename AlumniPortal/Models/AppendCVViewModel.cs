using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlumniPortal.Models
{
    public class AppendCVViewModel
    {
        public int cv_id { get; set; }

        [Display(Name = "Experiences")]
        public List<string> Experiences { get; set; }

        [Display(Name = "Skills")]
        public List<string> Skills { get; set; }

        [Display(Name = "Educational Attainments")]
        public List<string> EducAttainments { get; set; }

        public string alum_num { get; set; }

        public AppendCVViewModel()
        {
            Experiences = new List<string>();
            Skills = new List<string>();
            EducAttainments = new List<string>();
        }
    }
}
