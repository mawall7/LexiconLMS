using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class UserCourse
    {
        //denna är en string pga att den sparas som en GUID i DBn AspNetUsers
        public string ApplicationUserId { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
