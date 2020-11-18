using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class ApplicationUserModule
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string ApplicationUserId { get; set; }

        //Navigation property
        public Module Module { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
