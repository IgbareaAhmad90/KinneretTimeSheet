using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models.ViewModels
{
    public class CourseDetails
    {
        public Course course { get; set; }
        public List<UserCourse> mentors;

        public CourseDetails(Course course, List<UserCourse> mentors)
        {
            this.course = course;
            this.mentors = mentors;
        }
    }
}
