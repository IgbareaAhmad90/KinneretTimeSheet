using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("שם הקורס")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("שם המרצה")]
        public string LecturerName { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("אימייל המרצה")]
        public string LecturerEmail { get; set; }

        [Required]
        [DisplayName("מספר הקורס")]
        public string Key { get; set; }

        public  ICollection<UserCourse> UserCourses { get; set; }
    }
}
