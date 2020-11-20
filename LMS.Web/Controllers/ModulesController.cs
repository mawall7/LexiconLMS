using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;
using LMS.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using LMS.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using LMS.Data.Data;

namespace LMS.Web.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<ApplicationUser> userManager;
        public UserManager<ApplicationUser> UserManager { get; }

        public ModulesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.UserManager = userManager;
        }

        // GET: Modules
        //public async Task<IActionResult> Index()
        //{
        //    var userId = userManager.GetUserId(User);
        //    var model = new IndexViewModel
        //    {
        //         Modules = await _context.Modules.Include(g => g.AttendedMembers)
        //                               .Select(g => new ModulesViewModel
        //                               {
        //                                   Id = g.Id,
        //                                   Name = g.Name,
        //                                   StartDate = g.StartDate,
        //                                   EndDate = g.EndDate,
        //                                   Attending=g.AttendedMembers.Any(m=>m.ApplicationUserId==userId)
        //                               }).ToListAsync()

        //    };


        //    return View(model);
        //}
        public async Task<IActionResult> Index()
        {
            return View(await _context.Modules.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            if (Request.IsAjax())
                return PartialView("CreatePartial");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,CourseId")] Module @module)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@module);
                await _context.SaveChangesAsync();

                if (Request.IsAjax())
                {
                    var model = new ModulesViewModel
                    {
                        Id = module.Id,
                        Name = module.Name,
                        StartDate = module.StartDate,
                        EndDate = module.EndDate

                    };

                    return PartialView("ModulePartial", model);
                }


                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        // GET: Modules/Edit/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            return View(@module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,CourseId")] Module @module)
        {
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        // GET: Modules/Delete/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }

        public async Task<IActionResult> UserCourse()
        {

            //Get user
            var user = await UserManager.GetUserAsync(User);
            if (user is null)
            {
                //redirect to a "Login or reister"-page if not logged in
                return RedirectToAction(nameof(Index));
                //return BadRequest();

            }


            var modules = await _context.Modules
                .Include(a => a.Activities)
                .Include(a => a)
                //.Where(a => a.CourseId == user.CourseId)
                .ToListAsync();
            foreach (var mod in modules)
            {

            }
            var activities = await _context.Activities
               .Include(at => at.ActivityType)
               .ToListAsync();

            //var activityTypes = await _context.ActivityTypes
            //    .Include(at => at.Activities)
            //   .Where(at => at.Id == user.c)
            //   .ToListAsync();
            //Student course Information
            var model = await _context.Courses
               .Include(c => c.Modules)
               .ThenInclude(c => c.Activities)
               .Select(d => new ModulesViewModel
               {
                   Id = d.Id,
                   Name = d.Name,
                   Modules = modules,
                   Activities = activities

               })
               //.OrderBy()
               .FirstOrDefaultAsync(c => c.Id == user.CourseId);


            return View(model);


        }

        public async Task<IActionResult> UserCourseX()
        {

            //Get user
            var userId = UserManager.GetUserId(User);
            var Student = await OnGetAsync(2);
            //Student course Information
            var model = await _context.Courses
               .Include(c => c.Modules)
               .Include(c => c.Activities)
               .Select(c => new ModulesViewModel
               {
                   Id = c.Id,
                   // Name = c.Name,


               }).ToListAsync();

            return View(model);


        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Courses
           .Include(s => s.Modules)
           .ThenInclude(e => e.Activities)
           .AsNoTracking()
           .FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

    }
}
