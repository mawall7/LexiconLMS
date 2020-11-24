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
using LMS.Core.ViewModels;


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
        //public int SaveTest { get; set; }
        // GET: Activities
        //public async Task<IActionResult> Index2()
        //{

        //    //var applicationDbContext = _context.Activity.Include(a => a.ActivityType).Include(a => a.Module);
        //    //applicationDbContext.Select()
        //    return View();
        //}

        //public async Task<IActionResult> Index(int? Id)//Index(int DropitemModuleId)
        //{

        public async Task<IActionResult> Index(int? Id) // Erase! = 1 when theres a link from CourseListView for a modules 
        {
           // SaveTest = 1;
            if (Id == null)
                return NotFound();


            var model = _context.Activities
                .Where(a => a.ModuleId == Id)
                .Select(a => new ActivitiesViewModel
                {
                    Name = a.Name,
                    Id = a.Id,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Description = a.Description,
                    //ModuleId = a.ModuleId, ?
                    //ActivityTypeId = a.ActivityTypeId,
                    ActivityTypeName = a.ActivityType.Name
                }).ToList();
                
                
            
             
            

            return View(model);
          
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
          /*  try
            {
                var x = SaveTest / 2;
            }
            catch (DivideByZeroException x)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }*/

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


            ViewData["ActivityTypeName"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name"); //don't remove
            var model = new Activity { ModuleId = 4 };

            return View(model);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Activity activity)
        {

            if (_context.Activities.Any(a => a.Name == activity.Name && a.ModuleId == activity.ModuleId && a.StartTime == activity.StartTime) == false)
                {

                if (ModelState.IsValid)
                {
                    /* var activity = new Activity {
                         Description = Description}*/
                    activity = new Activity {
                        Name = activity.Name,
                        ActivityTypeId = activity.ActivityTypeId,
                        Description = activity.Description,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ModuleId = activity.ModuleId,
                        // ActivityType =
                    };
                    _context.Add(activity);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new {id=activity.ModuleId }); //returns to Index with moduleid to show activities in the module
                }
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Id", activity.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Set<Module>(), "Id", "Id", activity.ModuleId);
            ViewData["Exists"] = "This Activity allready exists!";
            return RedirectToAction(nameof(Create));//return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            var ActivityTypes = _context.ActivityTypes.ToList();
            if (activity == null)
            {
                return NotFound();
            }

            var ViewModel = new ActivityAndTypesViewModel
                          {
                              Activity = activity,
                              ActivityTypes = ActivityTypes
                          };
                
              return View(ViewModel);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(/*[Bind("Activity.Id,Activity.ActivityTypeId, Activity.ModuleId, Activity.Name,Activity.Description,Activity.StartTime,Ativity.EndTime")] */ActivityAndTypesViewModel currentActivity)
        {

            //var currentActivity = await _context.Activities
            // .FirstOrDefaultAsync(a => a.Id == id);
            var hit = _context.Activities.FirstOrDefaultAsync(a => a.Id == currentActivity.Activity.Id);

            var x = currentActivity.Activity;
            //    var activity = new Activity
            //{
                
            //    Name = currentActivity.Activity.Name,
            //    StartTime = currentActivity.Activity.StartTime,
            //    EndTime = currentActivity.Activity.EndTime,
            //    ModuleId = currentActivity.Activity.ModuleId,
            //    ActivityTypeId = currentActivity.Activity.ActivityTypeId,
            //    Description = currentActivity.Activity.Description
                
            //};
            //currentActivity.Activity.Name,
            //currentActivity.Activity.Name,
            //int? id = currentActivity.Activity.Id;
                
            if (currentActivity.Activity.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(x);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(currentActivity.Activity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //var a = _context.Activities
                  //  .FirstOrDefault(a => a.Id == id).ModuleId;
                    
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(currentActivity);
            //return View();// View(currentActivity);
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
