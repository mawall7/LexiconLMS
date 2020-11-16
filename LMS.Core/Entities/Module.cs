using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class Module
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //Foreign Key
        public int CourseId { get; set; }
        //Navigation property
        public ICollection<Activity> Activities { get; set; }

        //public ICollection<Document> Documents { get; set; }
        public ICollection<ApplicationUserModule> AttendedMembers { get; set; }
    }
}
