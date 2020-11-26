using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;
using LMS.Data.Data;
using LMS.Web.Models.ViewModels;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Web.Controllers {
    public class DocumentsController : Controller {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public DocumentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            db = context;
            this.userManager = userManager;
        }

        // GET: Documents
        public async Task<IActionResult> Index() {
            var model = db.Documents.Include(d => d.Activity).Include(d => d.ApplicationUser).Include(d => d.Course).
                Include(d => d.Module).Select(p => new DocumentViewModel
                {
                    Id = p.Id,
                    CourseName = p.Course.Name,
                    ActivityName = p.Activity.Name,
                    ModuleName = p.Module.Name,
                    DateCreated = p.DateCreated,
                    StartDate = p.Module.StartDate,
                    EndDate = p.Module.EndDate,
                    StartTime = p.Activity.StartTime,
                    EndTime = p.Activity.EndTime,
                   
                });

            return View("Index2", await model.ToListAsync());
        }







        public ActionResult RaedTextFile(int id) {
            var dbPath = db.Documents.Select(p => new Document { Id = p.Id, Path = p.Path }).Where(i => i.Id == id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), dbPath.FirstOrDefault().Path); //here supposed to get path from data base as string
            var contents = "";

            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            return Content(contents);
        }

















        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var document = await db.Documents
                .Include(d => d.Activity)
                .Include(d => d.ApplicationUser)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        //[Authorize(Roles = "Teacher")]
        public IActionResult Create(int? param ) {
            ViewData["ActivityId"] = new SelectList(db.Activities, "Id", "Id");
            ViewData["ApplicationUserId"] = new SelectList(db.Users, "Id", "Id");
            ViewData["ApplicationUserFirstName"] = new SelectList(db.Users, "FirstName", "FirstName");
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Id");
            ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Id");
            
            var model = new Document { CourseId = param };
            return View(model);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DateCreated,ApplicationUserId,CourseId,ActivityId,ModuleId,Path,Files")] Document document,  List<IFormFile> files) {
            document.DateCreated = DateTime.Now;

           
            ViewData["ActivityId"] = new SelectList(db.Activities, "Id", "Id", document.ActivityId);
            ViewData["ApplicationUserId"] = new SelectList(db.Users, "Id", "Id", document.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Id", document.CourseId);
            ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Id", document.ModuleId);







            long size = files.Sum(f => f.Length);
            var stringPath = "";
            var filePaths = new List<string>();
                    var fileName = document.Name;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                  var FullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Document Material/" + fileName);
                    stringPath = FullFilePath;
                    var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(FullFilePath);

                    using (var stream = new FileStream(FullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }


            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(User);
                document.ApplicationUserId = userId;
                document.Path = filePaths.First();
                document.Name = fileName;
                //document.CourseId = id;
                //ToDo Save path 
                //Lägg till pathen till dokumentet vart vi kan hitta det på servern

                //SET IDENTITY_INSERT db.Documents ON
                
                db.Add(document);
                await db.SaveChangesAsync();

               
                


                return  RedirectToAction("CourseList", "Courses");
            }
            // return View(document);
            return RedirectToAction("Index", "Home");
        }
        










    

    // GET: Documents/Edit/5
    public async Task<IActionResult> Edit(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(db.Activities, "Id", "Id", document.ActivityId);
            ViewData["ApplicationUserId"] = new SelectList(db.Users, "Id", "Id", document.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Id", document.CourseId);
            ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Id", document.ModuleId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateCreated,ApplicationUserId,CourseId,ActivityId,ModuleId")] Document document) {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(document);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            ViewData["ActivityId"] = new SelectList(db.Activities, "Id", "Id", document.ActivityId);
            ViewData["ApplicationUserId"] = new SelectList(db.Users, "Id", "Id", document.ApplicationUserId);
            ViewData["ApplicationUserFirstName"] = new SelectList(db.Users, "FirstName", "FirstName", document.ApplicationUser.FirstName);
            ViewData["ApplicationUserLastName"] = new SelectList(db.Users, "LastName", "LastName", document.ApplicationUser.LastName);
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Id", document.CourseId);
            ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Id", document.ModuleId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var document = await db.Documents
                .Include(d => d.Activity)
                .Include(d => d.ApplicationUser)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var document = await db.Documents.FindAsync(id);
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id) {
            return db.Documents.Any(e => e.Id == id);
        }

        /******************************* CourseMainPageDocument******************************/

        public async Task<IActionResult> CourseMainPageDocument(int? id) {
            var model = db.Documents.Include(d => d.Activity).Include(d => d.ApplicationUser).Include(d => d.Course).
                Include(d => d.Module).Select(p => new DocumentViewModel
                {
                    Id = p.Id,
                    CourseName = p.Course.Name,
                    CourseId = p.Course.Id
                    // }).Where(m => m.CourseId == id);
                }).FirstOrDefaultAsync(m => m.CourseId == id);




            // return View("CourseMainPageDocument", await model.ToListAsync());
            return View("CourseMainPageDocument", await model);
        }


        /******************************* CoursesListMaterial******************************/


        public async Task<ActionResult> CoursesListMaterial(int? id) {
            var model = db.Documents.Include(e => e.ApplicationUser).Include(e => e.Course).Select(p => new DocumentViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CourseName = p.Course.Name,
                CourseId = p.Course.Id,
                ModuleId = p.Module.Id,

                ApplicationUserFirstName = p.ApplicationUser.FirstName,
                ApplicationUserLastName = p.ApplicationUser.LastName,
                DateCreated = p.DateCreated,
                StartDate = p.Course.StartDate,
                EndDate = p.Course.EndDate,
                StartTime = p.Activity.StartTime,
                EndTime = p.Activity.EndTime
                //  }).Where(i => Documents.Contains(i.CourseId)); //i.CourseId
            }).Where(i => i.CourseId == id);

            return View("CoursesListMaterial", await model.ToListAsync()); //FirstOrDefaultAsync(m => m.Id == id);
        }



        /******************************* FileUpload******************************/

        


            
          






    }
}