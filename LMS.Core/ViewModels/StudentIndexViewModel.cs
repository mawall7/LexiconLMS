using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class StudentIndexViewModel
    {
        public int Id { get; set; }
        //public string UserId { get; set; }
        public int? CourseId { get; set; }

        //[DisplayName("Course Name")]
        //public string Name { get; set; }
        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }

        //Navigation Property
        public Course Course { get; set; }
        //public ICollection<Module> Modules { get; set; }
        //public ICollection<Activity> Activities { get; set; }
    }
}
