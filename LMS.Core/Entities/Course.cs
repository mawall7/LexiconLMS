using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Course Name")]
        [StringLength(maximumLength:50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:250, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        //Navigation Property
       
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }

        // AttendedCourse
        public ICollection<ApplicationUserCourse> AttendedCourse { get; set; }

        //Navigation property
        // kopplingstabell i UserCourse, vilka studenter deltar i kursen
        //public ICollection<UserCourse> AttendingStudents { get; set; }

        // public ICollection<Document> Documents { get; set; }

    }
}
