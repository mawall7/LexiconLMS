using LMS.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Controllers {
    public class ProjectRolesController : Controller {

        private RoleManager<IdentityRole> _roleManager;


        public ProjectRolesController(RoleManager<IdentityRole> roleManager) {

            this._roleManager = roleManager;

        }




        public IActionResult Index() {
            return View();
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole role) {

            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
            if(!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));

            }
            
            return View();
        }


    }
}
