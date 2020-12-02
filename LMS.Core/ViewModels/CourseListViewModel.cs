using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class CourseListViewModel
    {
        public int Id { get; set; }

        [DisplayName("Course Name")]
        public string Name { get; set; }
        
        public string Description { get; set; }

        
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }


 

        public ICollection<Module> Modules { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }
        public CourseDetailsViewModel CourseDetails { get; set; }
        public ModuleDetailsViewModel ModuleDetails { get; set; }
    }
}
