using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.Entities
{
    public class Module
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }


        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Course Course { get; set; }

        //Foreign Key
        public int CourseId { get; set; }
        //Navigation property
        public ICollection<Activity> Activities { get; set; }

        //public ICollection<ApplicationUserModule> AttendedMembers { get; set; }
        //public ICollection<Document> Documents { get; set; }
        public ICollection<ApplicationUserModule> AttendedMembers { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
