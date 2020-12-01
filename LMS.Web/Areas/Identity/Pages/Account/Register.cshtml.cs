using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LMS.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using LMS.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LMS.Web.Areas.Identity.Pages.Account {
    [AllowAnonymous]
    
/****/  public class RegisterModel : PageModel {
        public List<SelectListItem> CoursesList { get; }
        public List<SelectListItem> CoursesIdList { get; }

        public Dictionary<SelectListItem, SelectListItem> Dictionary;
        
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
        Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
         ApplicationDbContext context,

        SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender) {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

            //Fatma  add Courses name to select list
            _context = context;
            CoursesList = _context.Courses
                         //.Select(m => m.Name) // to do finish 
                          .Select(i => new { i.Name, i.Id })
                         .Distinct()
                         .Select(m => new SelectListItem
                         {
                             Text = m.Name.ToString(),
                             Value = m.Id.ToString()
                         })
                         .ToList();


            //CoursesIdList = _context.Courses
            //                .Select(m => m.Id) // to do finish 
            //                 .Distinct()
            //                 .Select(m => new SelectListItem
            //                 {
            //                     Text = m.ToString(),
            //                     Value = m.ToString()
            //                 })
            //                 .ToList();





            // Dictionary = CoursesList.Zip(CoursesIdList, (k, v) => new { CoursesList = k, CoursesIdList = v })
            //                     .ToDictionary(x => x.CoursesList, x => x.CoursesIdList);








        }




        [BindProperty]
        public InputModel Input { get; set; }
       

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

       
 /*****/    public class InputModel {
            //Soile
            //Add Firstname and lastname to the Register page
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }

            //Fatma
            //Add courses to Register page
          
            [Display(Name = "Course Name")]
            public string CourseName { get; set; }

            [Display(Name = "Choose User Role")]
            public string role { get; set; }
            public int CourseId { get; set; }



            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null) {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync( string returnUrl = null) {
            if (Input.role is null)
            {
                throw new ArgumentNullException(nameof(Input.role));
            }

            //returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {


                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName, //added by Fatma
                    LastName = Input.LastName,   // 
                    CourseId = Input.CourseId,   // 
                };



                if (Input.role == "0") Input.Password = "LmsLexicon20?";
                var result = await _userManager.CreateAsync(user, Input.Password);
               
               
                if (result.Succeeded)
                {
                    if (Input.role == "0")
                    {
                        _userManager.AddToRoleAsync(user, "Teacher").Wait();
                     
                    }
                    else if (Input.role == "1")
                        _userManager.AddToRoleAsync(user, "Student").Wait();

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(returnUrl);
                        return RedirectToAction("Index");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
