using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FairShareAPI.Models
{
    public partial class Model1 : DbContext
    {
        /*
        public Model1()
            : base("name=Model2")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblGroup>()
                .Property(e => e.fldGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<tblLogin>()
                .Property(e => e.fldPassword)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.fldEmail)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.fldFirstName)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.fldLastName)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserExpenses>()
                .Property(e => e.fldNote)
                .IsUnicode(false);
        }
        */
    }
}
