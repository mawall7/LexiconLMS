using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;
using LMS.Data.Data;
using LMS.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LMS.Core.ViewModels;
using AutoMapper;

namespace LMS.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        public UserManager<ApplicationUser> UserManager { get; }

        public CoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
            //manage users and their roles
            UserManager = userManager;
        }

        // GET: Courses
        [AllowAnonymous]
        public async Task<IActionResult> 
            Index(IndexViewModel viewModel = null)
        {
            //Get user
            var user = UserManager.GetUserId(User);

            var courses = _context.Courses
           .Include(m => m.Modules)
           .AsNoTracking();
            return View(await courses.ToListAsync());

        }
        //Get Student Course, modules and activities



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

            //Student course Information
            var model = await _context.Courses
               .Include(c => c.Modules)
               .ThenInclude(c => c.Activities)
               .Select(d => new StudentCourseViewModel
               {
                   Id = d.Id,
                   Name = d.Name,
                   StartDate = d.StartDate,
                   EndDate = d.EndDate,
                   Modules = d.Modules,
                   Activities = d.Activities,
                   AttendingStudents = d.ApplicationUsers

               })
               //.OrderBy()
               .FirstOrDefaultAsync(c => c.Id == user.CourseId);


            if (model == null)
            {
                //redirect to a "Welcome Student"-page if not logged in
                return RedirectToAction(nameof(Index));
                //return BadRequest();

            }
            return View(model);


        }

        // GET: CourseList
        public async Task<IActionResult> CourseList()
        {
            var model = await _context.Courses
                .Include(c => c.Modules)
                .Include(c => c.Activities)
                .Select(c => new CourseListViewModel
                {
                    Id = c.Id,
                    Name = c.Name


                }).ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> Index2()
        {
            return View(await _context.Courses.ToListAsync());
        }


        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.Courses
                .Select(c => new CourseDetailsViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Modules = c.Modules
                })
                .FirstOrDefaultAsync(c => c.Id == id);
            if (courseModel == null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel createCourseViewModel)
        {

            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(createCourseViewModel);
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createCourseViewModel);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }


    }
}
