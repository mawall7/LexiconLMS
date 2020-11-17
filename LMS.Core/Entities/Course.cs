using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //Navigation Property
        public  ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Module> Modules { get; set; }

       // public ICollection<Document> Documents { get; set; }

    }
}
