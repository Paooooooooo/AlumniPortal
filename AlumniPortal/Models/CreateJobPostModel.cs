using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class CreateJobPostModel
    {
        public DbSet<job_post_tbl> job_post_tbl { get; set; }
        public DbSet<comp_tbl> comp_tbl { get; set; }
    }
}