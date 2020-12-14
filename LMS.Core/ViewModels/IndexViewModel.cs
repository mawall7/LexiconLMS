using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        //Lista över Kurs man deltar (/ deltagit i)
        //public IEnumerable<CourseViewModel> Courses { get; set; }
        public int? CourseId { get; set; }
        //Navigation property
        public Course Course { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public bool Attending { get; set; }
    }
}
