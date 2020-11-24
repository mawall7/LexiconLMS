using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class AttendingStudentsViewModel
    {
        public int Id { get; set; }
        //public string ApplicationUserId { get; set; }
        public int? CourseId { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
