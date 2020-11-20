using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;
using LMS.Data.Data;

namespace LMS.Web.Views.Student
{
    public class IndexModel : PageModel
    {
        private readonly LMS.Data.Data.ApplicationDbContext _context;

        public IndexModel(LMS.Data.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Courses.ToListAsync();
        }
    }
}
