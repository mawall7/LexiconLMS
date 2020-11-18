using System;
using LMS.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS.Core.ViewModels;

namespace LMS.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string> {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }

        //Soile Test
        public DbSet<ApplicationUser> Student { get; set; }
        public DbSet<LMS.Core.Entities.Course> CourseForStudent { get; set; }
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            //specar att detta är primärnyckeln - PK, en Kompositnykel av k.ApplicationUserId, k.GymClassId, som är unik kombo i kopplingstbl
            base.OnModelCreating(builder);
            //builder.Entity<UserCourse>().HasKey(k => new { k.ApplicationUserId, k.CourseID });
        }
       


        public DbSet<Course> Course { get; set; }
        //public DbSet<Activity> Activities { get; set; }
      
    }
}
