using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Course Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public ICollection<Module> Modules { get; set; }
    }
}
