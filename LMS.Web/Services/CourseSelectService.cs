using LMS.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Services
{
    public class CourseSelectService : ICourseSelectServices
    {
        private readonly ApplicationDbContext _context;

        public CourseSelectService(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IEnumerable<SelectListItem>> GetCourseAsync()
        {
            return await _context.Courses
                .Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString()
                })
                .ToListAsync();
        }
    }
}
