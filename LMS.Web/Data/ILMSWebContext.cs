using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;

namespace LMS.Web.Data
{
    public class ILMSWebContext : DbContext
    {
        public ILMSWebContext (DbContextOptions<ILMSWebContext> options)
            : base(options)
        {
        }

        public DbSet<LMS.Core.Entities.Course> Course { get; set; }

        public DbSet<LMS.Core.Entities.Module> Module { get; set; }
    }
}
