using LMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.ViewModels
{
    public class ActivityAndTypesViewModel
    {
        public Activity Activity { get; set; }

        public List<ActivityType> ActivityTypes{get; set;}
        //public IEnumerable <SelectListItem> ActivityTypes{ get; set;}
    }
}
