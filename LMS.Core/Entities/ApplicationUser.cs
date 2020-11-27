using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace LMS.Core.Entities {
    public class ApplicationUser : IdentityUser
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
      
        public string Address { get; set; }
        public string Phone { get; set; }

        public int? CourseId { get; set; }
        //Navigation property
        public Course Course { get; set; }
        // public ICollection<Document> Documents { get; set; }

        // AttendedCourse
        public ICollection<ApplicationUserCourse> AttendedCourse { get; set; }


    }
}
