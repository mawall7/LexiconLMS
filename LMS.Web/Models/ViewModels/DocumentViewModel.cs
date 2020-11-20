using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models.ViewModels {
    public class DocumentViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CourseName { get; set; }
        public string? ModuleName { get; set; }
        public string? ActivityName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string ApplicationUserFullName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }
}
