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


                var fake = new Faker("sv");



                // ---------------------------------------Courses  SeedData----------------------------------------
               if (context.Courses.Any()) return;
              
               {
                    context.Courses.RemoveRange(context.Courses);
                    context.SaveChanges();
                }
            

                var courses = new List<Course>();

                for (int i = 0; i < 5; i++)
                {
                    var course = new Course
                    {

                        Name = fake.Music.Genre(),
                        Description = fake.Lorem.Sentence(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(fake.Random.Int(2, 1))
                    };

                    courses.Add(course);
                }



                await context.AddRangeAsync(courses);
                await context.SaveChangesAsync();



                // ---------------------------------------Users  SeedData----------------------------------------

              


                var aspNetUsers = new List<ApplicationUser>();

                for (int i = 0; i < 10; i++)
                {
                    var APPUser = new ApplicationUser
                    {

                        FirstName = fake.Name.FirstName(),
                        LastName = fake.Name.LastName(),
                        Address = fake.Address.FullAddress() ,
                        Email = fake.Internet.Email(),
                        Phone = fake.Phone.PhoneNumber(),
                        CourseId = courses[fake.Random.Int(1, courses.Count) - 1].Id,
                        
                    };

                    aspNetUsers.Add(APPUser);
                }



                await context.AddRangeAsync(aspNetUsers);
                await context.SaveChangesAsync();


                // ---------------------------------------ActivityType  SeedData----------------------------------------
                if (context.ActivityTypes.Any())
                {
                    context.ActivityTypes.RemoveRange(context.ActivityTypes);
                    context.SaveChanges();
                }



                var activityTypes = new List<ActivityType>();


                for (int i = 1; i <= 4; i++)
                {
                    var activityType = new ActivityType
                    {

                        Name = fake.Music.Genre(),


                    };

                    activityTypes.Add(activityType);
                }

                await context.AddRangeAsync(activityTypes);
                await context.SaveChangesAsync();
                // ---------------------------------------Module  SeedData----------------------------------------
                if (context.Modules.Any())
                {
                    context.Modules.RemoveRange(context.Modules);
                    context.SaveChanges();
                }


                var modules = new List<Module>();

                for (int i = 0; i < 4; i++)
                {
                    var module = new Module
                    {

                        Name = fake.Music.Genre(),
                        Description = fake.Lorem.Sentence(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(fake.Random.Int(3, 4)),
                        CourseId = courses[fake.Random.Int(1,courses.Count) - 1].Id
                    };

                    modules.Add(module);
                }



                await context.AddRangeAsync(modules);
                await context.SaveChangesAsync();


                // ---------------------------------------Activity  SeedData----------------------------------------
                if (context.Activities.Any())
                    context.Activities.RemoveRange(context.Activities);



                var activities = new List<Activity>();


                for (int i = 1; i <= 4; i++)
                {
                    var activity = new Activity
                    {

                        Name = fake.Music.Genre(),
                        Description = fake.Lorem.Sentence(),
                        StartTime = DateTime.Now.AddDays(fake.Random.Int(0, 2)),
                        EndTime = DateTime.Now.AddDays(fake.Random.Int(2, 4)),
                        ActivityTypeId = activityTypes[fake.Random.Int(1, activityTypes.Count) - 1].Id,
                        ModuleId = modules[fake.Random.Int(1, modules.Count) - 1].Id


                    };

                    activities.Add(activity);
                }



                await context.AddRangeAsync(activities);
                await context.SaveChangesAsync();

                // ---------------------------------------Dokument  SeedData----------------------------------------
                if (context.Documents.Any())
                {
                    context.Documents.RemoveRange(context.Documents);
                    context.SaveChanges();
                }


                var documents = new List<Document>();

                for (int i = 0; i < 10; i++)
                {
                    var document = new Document
                    {

                        Name = fake.Music.Genre(),
                        Description = fake.Lorem.Sentence() ,
                        DateCreated = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        CourseId = courses[fake.Random.Int(1, courses.Count) - 1].Id,
                        ApplicationUserId = aspNetUsers[fake.Random.Int(1, aspNetUsers.Count) - 1].Id,
                        ActivityId = activities[fake.Random.Int(1, activities.Count) - 1].Id,
                        Path = fake.Image.PicsumUrl(),
                        ModuleId = modules[fake.Random.Int(1, modules.Count) - 1].Id

                    };

                    documents.Add(document);
                }



                await context.AddRangeAsync(documents);
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

                var teacherEmail = "teacher@lms.se"; // LexiconLms20?

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

                //ToDo only give teacher role teacher
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

