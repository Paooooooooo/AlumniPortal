using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class CompanyJobPostsViewModel
    {
        public List<job_post_tbl> JobPosts { get; set; }
        public List<JobTargetViewModel> JobTargets { get; set; }
    }
}