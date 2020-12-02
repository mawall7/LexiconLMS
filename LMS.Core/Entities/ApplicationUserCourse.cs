using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class ApplicationUserCourse
    {
        public int CourseId { get; set; }
        public string ApplicationUserId { get; set; }

        //Navigation property
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
