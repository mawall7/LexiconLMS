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
using LMS.Web.Extensions;

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
          

            var @activity = await _context.Activities.FirstOrDefaultAsync(m => m.Id == id);
            //.FirstorDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(@activity);

        }
        public IActionResult Create(int id)
        {
            if (Request.IsAjax())
            {
                var activity = new Activity
                {
                    Module = new Module
                    {
                        CourseId = id
                    }
                };
                return PartialView("CreatePartial", activity);
            }
            return View();
            ////for now it creates only for first module in modules table EF 
            //var ModuleId = _context.Modules.Select(a=> a.Id).FirstOrDefault();


            //ViewData["ActivityTypeName"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name"); //don't remove
            //var model = new Activity { ModuleId = ModuleId };

            //return View(model);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Activity @activity)
        {

            //if (_context.Activities.Any(a => a.Name == activity.Name && a.ModuleId == activity.ModuleId && a.StartTime == activity.StartTime) == false)
            //    {

            //    if (ModelState.IsValid)
            //    {
            //        /* var activity = new Activity {
            //             Description = Description}*/
            //        activity = new Activity {
            //            Name = activity.Name,
            //            ActivityTypeId = activity.ActivityTypeId,
            //            Description = activity.Description,
            //            StartTime = activity.StartTime,
            //            EndTime = activity.EndTime,
            //            ModuleId = activity.ModuleId,
            //            // ActivityType =
            //        };
            //        _context.Activities.Add(activity);

            //        await _context.SaveChangesAsync();
            //        return RedirectToAction("Index", new {id=activity.ModuleId }); //returns to Index with moduleid to show activities in the module
            //    }
            //}
            ////if an activity allready exist
            //// ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Id", activity.ActivityTypeId);
            //ViewData["ActivityTypeName"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name"); //don't remove            
            //ViewData["ModuleId"] = new SelectList(_context.Set<Module>(), "Id", "Id", activity.ModuleId);
            //ViewData["Exists"] = "This Activity allready exists!"; //Use Remote istället? 
            // return View(activity);//return RedirectToAction(nameof(Create));<p>@Html.Raw(ViewData["Exists"])</p>


            if (ModelState.IsValid)
            {
                _context.Add(@activity);
                await _context.SaveChangesAsync();

                if (Request.IsAjax())
                {
                    var model = new ActivitiesViewModel
                    {
                        Id = activity.Id,
                        Name = activity.Name,
                        Description = activity.Description,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ModuleId = activity.ModuleId,
                        ActivityTypeId = activity.ActivityTypeId

                    };

                    return PartialView("ActivityPartial", model);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(@activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)  //activity id
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

            ViewData["ActivityTypeName"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name"); //don't remove

            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id, Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Activity activity)
        {

            bool hit = _context.Activities.Any(a => a.Id == id);

            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid && hit == true)
            {
                try
                {
                    _context.Activities.Update(activity);
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



                return RedirectToAction("Index", new { id = activity.ModuleId });
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeName"] = new SelectList(_context.Set<ActivityType>(), "Id", "Name");
            return RedirectToAction("Edit", new { id = activity.Id });//return View(activity);
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


        public async Task<IActionResult> HomeWork() {
            //var item = UserManager.GetUsersInRoleAsync("Student");
            var model = _context.Activities.Select(P => P.Name == "Home Work");
         



            return View();
        }








        public async Task<IActionResult> DaedLine(int? Id) 
       {


            var model = _context.Activities

                .Select(a => new ActivitiesViewModel
                {
                   
                    Id = a.Id,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    ActivityTypeName = a.ActivityType.Name,
                    ModuleName = a.Module.Name
                }).Where(e => e.ModuleName == "Home Work").FirstOrDefault();






            return View("DeadLine", model);

        }












    }
}
