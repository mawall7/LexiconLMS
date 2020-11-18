using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class UserCourse
    {
        public int UserCourseID { get; set; }
        public int CourseID { get; set; }
        
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
