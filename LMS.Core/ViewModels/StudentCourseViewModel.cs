using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        [DisplayName("Course")]
        public string Name { get; set; }

        [DisplayName("StartDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayName("EndDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }
        //Navigation property
        public bool Enrolled { get; set; }
        //Navigation Property
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ApplicationUser> AttendingStudents { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
