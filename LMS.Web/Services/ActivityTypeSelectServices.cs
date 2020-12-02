using LMS.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Services
{
    public class ActivityTypeSelectServices : IActivityTypeSelectServices
    {
        private readonly ApplicationDbContext _context;

        public ActivityTypeSelectServices(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IEnumerable<SelectListItem>> GetActivityTypeAsync()
        {
            return await _context.ActivityTypes
                .Select(a => new SelectListItem
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString()
                })
                .ToListAsync();
        }
    }
}
