using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class TeacherCourseViewModel
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        [DisplayName("Course Name")]
        public string Name { get; set; }

        //Navigation property
        public bool Enrolled { get; set; }
        //Navigation Property
        //public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
