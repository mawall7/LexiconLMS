using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Course")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Navigation Property
        [Display(Name = "Student name")]
        public ICollection<ApplicationUser> Attendents { get; set; }
        //Modules connected to the course
        public int? CourseId { get; set; }

        public Course Course { get; set; }
        public ICollection<Module> Modules { get; set; }

    }
}
