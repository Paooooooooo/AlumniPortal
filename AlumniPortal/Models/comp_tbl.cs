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
    
    public partial class comp_tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public comp_tbl()
        {
            this.job_post_tbl = new HashSet<job_post_tbl>();
        }
    
        public int comp_id { get; set; }
        public string comp_name { get; set; }
        public string comp_con_num { get; set; }
        public string comp_con_name { get; set; }
        public string comp_email { get; set; }
        public string comp_address { get; set; }
        public string comp_password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<job_post_tbl> job_post_tbl { get; set; }
    }
}