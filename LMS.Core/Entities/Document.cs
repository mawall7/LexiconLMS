using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities {
    public class Document {
        public int Id { get; set; }


        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        //FK
        public string ApplicationUserId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
        public int? ModuleId { get; set; }
        public Course Course { get; set; }
        public Activity Activity { get; set; }
        public Module Module { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        

    }
}
