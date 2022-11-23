using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FairShareAPI.Models;

namespace FairShareAPI.Data
{

	public class FairShareContext : DbContext
    {
        public FairShareContext (DbContextOptions<FairShareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tblGroup> tblGroup { get; set; }
        public virtual DbSet<tblLogin> tblLogin { get; set; }
        public virtual DbSet<tblReceipt> tblReceipt { get; set; }
        public virtual DbSet<tblTrip> tblTrip { get; set; }
        public virtual DbSet<tblTripToUserExpenses> tblTripToUserExpenses { get; set; }
        public virtual DbSet<tblUser> tblUser { get; set; }
        public virtual DbSet<tblUserExpenses> tblUserExpenses { get; set; }
        public virtual DbSet<tblUserToGroup> tblUserToGroup { get; set; }

    }

}
