using LMS.Core.Entities;
using LMS.Core.ViewModels;
using LMS.Data.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//@model LMS.Core.Entities.Activity


namespace LMS.Web.ViewComponents {
    public class ActivityViewComponent : ViewComponent {

        private ApplicationDbContext _context;
        public ActivityViewComponent(ApplicationDbContext context) {

            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync() {

            var model = _context.Activities
                 .Select(a => new Activity
                 {
                     Name = a.Name,
                     StartTime = a.StartTime,
                     EndTime = a.EndTime,

                     Module = a.Module
                 }).Where(e => e.Module.Name == "Home Work").FirstOrDefault();






            return View(model);
        }


    }
}
