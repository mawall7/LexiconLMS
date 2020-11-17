using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class ModulesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public bool Attending { get; set; }
    }
}
