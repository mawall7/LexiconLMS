using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("End Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        // Foreign key
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
        // Navigation property
        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
