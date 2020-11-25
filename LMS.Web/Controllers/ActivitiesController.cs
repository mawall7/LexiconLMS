using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;
using LMS.Data.Data;
using Microsoft.AspNetCore.Identity;

namespace LMS.Web.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserManager<ApplicationUser> UserManager;

        public ActivitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Activities
        //public async Task<IActionResult> Index2()
        //{

        //    //var applicationDbContext = _context.Activity.Include(a => a.ActivityType).Include(a => a.Module);
        //    //applicationDbContext.Select()
        //    return View();
        //}

        //public async Task<IActionResult> Index(int? Id)//Index(int DropitemModuleId)
        //{

        public async Task<IActionResult> Index()
        {
            return View(await _context.Activities.ToListAsync());
        }



        /*var userId = UserManager.GetUserId(User);
        var allaKurser = _context.Courses.ToList();
        var  courseU = _context.Courses.Find(userId,Id);//KursenFörAnvändaren*/


        //var applicationDbContext = _context.Activity.Include(a => a.ActivityType).Include(a => a.Module);
        //var model = _context.Activity.Include(a => a.Module) // lägga till db.set för Module istället?context.Course istället?

        //.Where(a => a.ModuleId == DropitemModuleId); 


        //.Where(a=> a.Module.Activities
        //return View();
        //   }

        // GET: Activities/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var activity = await _context.Activity
        //        .Include(a => a.ActivityType)
        //        .Include(a => a.Module)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (activity == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(activity);
        //}

        //// GET: Activities/Create

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @activity = await _context.Activities.FirstOrDefaultAsync(m => m.Id == id);
            //.FirstorDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(@activity);

        }
        public IActionResult Create()
        {

            /*ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Id");
            ViewData["ModuleId"] = new SelectList(_context.Set<Module>(), "Id", "Id");*/



            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Activity activity)
        {

            if (ModelState.IsValid)
            {
                _context.Add(activity);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Id", activity.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Set<Module>(), "Id", "Id", activity.ModuleId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Id", activity.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Set<Module>(), "Id", "Id", activity.ModuleId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Activity activity)
        {
            if (id != activity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.Id))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Id", activity.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Set<Module>(), "Id", "Id", activity.ModuleId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
