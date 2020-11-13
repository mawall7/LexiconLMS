using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;

namespace LMS.Web.Data
{
    public class LMSWebContext : DbContext
    {
        public LMSWebContext (DbContextOptions<LMSWebContext> options)
            : base(options)
        {
        }

        public DbSet<LMS.Core.Entities.Activity> Activity { get; set; }
    }
}
