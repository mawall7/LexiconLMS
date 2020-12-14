using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Data.Data;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            { 
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                var config = services.GetRequiredService<IConfiguration>();

                // var teacherPW = "LmsLexicon20?";//config["teacherPW"];
                //teacherPW saved in user-secret teacherPW = LmsLexicon20?
                var teacherPW = config["teacherPW"];

                try
                { 
                   //SeedData.InitializeAsync(services, teacherPW).Wait();
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e.Message, "Seed Fail");
                }

            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
