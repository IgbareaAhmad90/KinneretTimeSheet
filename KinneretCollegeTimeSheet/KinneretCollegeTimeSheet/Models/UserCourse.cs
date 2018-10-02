using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models
{
    public class UserCourse
    {
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        public  ICollection<Report> reports { get; set; }

        public string StatusMessage { get; set; }

    }
}
