using Bogus;
using LMS.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Data {
   public class SeedData {


        public static async Task InitializeAsync(IServiceProvider services, string teacherPW) {
            using (var context = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
               if (context.Courses.Any()) return;

                var fake = new Faker("sv");
                var activities = new List<Activity>();
                var courses = new List<Course>();
                var modules = new List<Module>();


                for (int i = 0; i < 3; i++)
                { 
                    var course = new Course
                    {

                        Name = fake.Music.Genre() ,
                        Description = fake.Hacker.Verb(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(fake.Random.Int(2, 1))
                    };

                    courses.Add(course);
                }



                await context.AddRangeAsync(courses);
                await context.SaveChangesAsync();




                var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManger = services.GetRequiredService<RoleManager<IdentityRole>>();

                var roleNames = new[] { "Teacher", "Student" };

                foreach (var roleName in roleNames)
                {
                    if (await roleManger.RoleExistsAsync(roleName)) continue;

                    var role = new IdentityRole { Name = roleName };
                    var result = await roleManger.CreateAsync(role);

                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

                }

                var teacherEmail = "teacher@lms.se";

                var foundUser = await userManger.FindByEmailAsync(teacherEmail);

                if (foundUser != null) return;

                var teacher = new ApplicationUser
                {
                    UserName = teacherEmail,
                    Email = teacherEmail,
                    FirstName = "Adam",
                    LastName = "Svensson",
                   // TimeOfRegistration = DateTime.Now
                };

                var addTeacherResult = await userManger.CreateAsync(teacher, teacherPW);

                if (!addTeacherResult.Succeeded) throw new Exception(string.Join("\n", addTeacherResult.Errors));

                var teacherUser = await userManger.FindByNameAsync(teacherEmail);

                foreach (var role in roleNames)
                {
                    if (await userManger.IsInRoleAsync(teacherUser, role)) continue;
                    var addToRoleResult = await userManger.AddToRoleAsync(teacherUser, role);
                    if (!addToRoleResult.Succeeded) throw new Exception(string.Join("\n", addToRoleResult.Errors));

                }

                await context.SaveChangesAsync();

            }


        }


































    }
}
