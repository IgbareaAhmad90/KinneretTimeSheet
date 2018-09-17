using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models.RegistrationModels
{
    public class AddUserCourseModel
    {

        [Required]
        public string UserID { get; set; }

        [Required]
        public int CourseID { get; set; }


    }
}
