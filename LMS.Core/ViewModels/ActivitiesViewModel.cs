using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class ActivitiesViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Description { get; set; }

        public int ModuleId { get; set; }
        
        public string ActivityTypeName { get; set; }
        // Uncomment 
        public int ActivityTypeId { get; set; }

        public ICollection<Activity> Activities{ get; set; }

        //public ICollection <ActivityType> ActivityTypesList{ get; set; }
    }
}
