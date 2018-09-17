using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("שם")]
        public string name { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("ת.ז")]
        public int studentId { get; set; }

        [Required]
        public int ReportID { get; set; }
        public Report Report { get; set; }
    }
}
