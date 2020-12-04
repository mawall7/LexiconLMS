using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using LMS.Core.Entities;

namespace LMS.Web.Models.ViewModels {
    public class DocumentViewModel {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ModuleId { get; set; }
        public int ActivityId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        [Display(Name = "Course Name")]
        public string? CourseName { get; set; }
        public string? ModuleName { get; set; }
        public Module Module { get; set; }
        public string? ActivityName { get; set; }
        public int? ModuleNumber { get; set; }
        public int? ActivityNumber { get; set; }
        
        public string CourseDescription { get; set; }
        public string ModuleDescription { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime DateCreated { get; set; }

        public string ApplicationUserFirstName { get; set; }
        public string ApplicationUser { get; set; }
        public string ApplicationUserLastName { get; set; }
        [Display(Name = "Created by")]
        public string ApplicationUserFullName => $"{ApplicationUserFirstName} {ApplicationUserLastName}";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }


    }
}
