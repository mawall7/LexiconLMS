using LMS.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Services
{
    public class ModuleSelectService : IModuleSelectService
    {
        private readonly ApplicationDbContext _context;

        public ModuleSelectService(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<SelectListItem>> GetModuleAsync(int id)
        {
            return await _context.Modules
                .Where(m => m.CourseId == id)
                .Select(m => new SelectListItem
                {
                    Text = m.Name.ToString(),
                    Value = m.Id.ToString()
                })
                .ToListAsync();
        }
    }
}
