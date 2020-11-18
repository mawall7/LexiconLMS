using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class CourseListViewModel
    {
        public int Id { get; set; }

        [DisplayName("Course Name")]
        public string Name { get; set; }

        //Navigation Property

        public ICollection<Module> Modules { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}