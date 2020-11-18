using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //Soile
        //public property, UserManager is only a getter
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
        public async Task<IActionResult> Index(IndexViewModel viewModel = null)
        {
            //Get user
            var userId = UserManager.GetUserId(User);

            var courses = _context.Courses
           .Include(m => m.Modules)
           .AsNoTracking();
            return View(await courses.ToListAsync());
           
        }

        //Get Student Course, modules and activities
        public async Task<IActionResult> UserCourse()
        {

            // //Get user
            var userId = UserManager.GetUserId(User);
            var model = await _context.Course
               .Include(c => c.Modules)
               .Include(c => c.Activities)
               .Select(c => new StudentIndexViewModel
               {
                   Id = c.Id,
                   Name = c.Name

               }).ToListAsync();

            return View(model);
           

        }
        // GET: CourseList
        public async Task<IActionResult> CourseList()
        {
            var model = await _context.Course
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
            var userId = UserManager.GetUserId(User);

            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
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
