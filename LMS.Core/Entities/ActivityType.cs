using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Navigation property
        public ICollection<Activity> Activities { get; set; }
    }
}
