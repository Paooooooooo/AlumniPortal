//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlumniPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cv_tbl
    {
        public int cv_id { get; set; }
        public string alum_num { get; set; }
        public string experiences { get; set; }
        public string skills { get; set; }
        public string educ_attain { get; set; }
    
        public virtual alum_tbl alum_tbl { get; set; }
    }
}