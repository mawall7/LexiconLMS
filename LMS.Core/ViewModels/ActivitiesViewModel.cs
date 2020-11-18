using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class ActivitiesViewModel
    {
        public ICollection<Module> Modules;
        public ICollection<Activity> Activities;

    }
}
