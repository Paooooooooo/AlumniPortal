using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class JobPostIndexModel
    {
        public DbSet<job_post_tbl> JobPosts { get; set; }
    }
}