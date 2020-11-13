using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        //public ActivityType ActivityType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        // Foreign key
        public int CourseId { get; set; }
        public int ActivityTypeId { get; set; }
        // Navigation property
       // public Document Document { get; set; }
    }
}
