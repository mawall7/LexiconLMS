using LMS.Core.Entities;
using LMS.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Controllers
{
    //[HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher")]
    public class UsersController : Controller
    {
        // Information from https://www.c-sharpcorner.com/UploadFile/asmabegam/Asp-Net-mvc-5-security-and-creating-user-role/
        private readonly ApplicationDbContext _context;
        public UserManager<ApplicationUser> UserManager { get; }
        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.UserManager = UserManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var userId = UserManager.GetUserId(User);
                // ApplicationDbContext context = new ApplicationDbContext();
                //UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                //var s = UserManager.OnGetAsync(userId);
                //var s = UserManager.OnGetAsync(1);
                //var s = UserManager.GetUsersInRoleAsync("Teacher");
                var s = UserManager.GetUsersInRoleAsync("Teacher");
                if (s.ToString()== "Teacher")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = await _context.UserRoles;

        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(model);
        //}
    }
}
