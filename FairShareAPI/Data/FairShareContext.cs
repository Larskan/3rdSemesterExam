using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FairShareAPI.Models;

namespace FairShareAPI.Data
{
#pragma warning disable CS1591
	public class FairShareContext : DbContext
    {
        public FairShareContext (DbContextOptions<FairShareContext> options)
            : base(options)
        {
        }

        public DbSet<FairShareAPI.Models.User> User { get; set; } = default!;

        public DbSet<FairShareAPI.Models.Group> Group { get; set; }

        public DbSet<FairShareAPI.Models.Login> Login { get; set; }

        public DbSet<FairShareAPI.Models.Receipt> Receipt { get; set; }

        public DbSet<FairShareAPI.Models.Trip> Trip { get; set; }

        public DbSet<FairShareAPI.Models.UserExpenses> UserExpenses { get; set; }
    }
#pragma warning restore CS1591
}
