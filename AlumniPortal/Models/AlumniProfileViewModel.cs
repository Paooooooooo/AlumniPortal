using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class AlumniProfileViewModel
    {
        [Key]
        public string AlumNum { get; set; }

        [Required]
        public string DeptId { get; set; }

        [Required]
        [StringLength(100)]
        public string AlumName { get; set; }

        [Required]
        [StringLength(10)]
        public string AlumSex { get; set; }

        [Required]
        public DateTime AlumBdate { get; set; }

        [Required]
        public int YrGrad { get; set; }

        [Required]
        [EmailAddress]
        public string AlumEmail { get; set; }

        [Required]
        [StringLength(15)]
        public string AlumCon { get; set; }
    }
}