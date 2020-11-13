
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace LMS.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
       // public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
       // public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }


        //Navigation property
        public ICollection<Course> Courses { get; set; }
       // public ICollection<Document> Documents { get; set; }
    }
}
