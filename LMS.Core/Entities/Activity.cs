﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        // Foreign key
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
        // Navigation property
        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
