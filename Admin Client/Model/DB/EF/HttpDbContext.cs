using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Admin_Client.Model.DB.EF
{
    public partial class HttpDbContext : DbContext
    {
        public HttpDbContext()
            : base("name=HttpDbContext")
        {
        }

        public virtual DbSet<tblGroup> tblGroups { get; set; }
        public virtual DbSet<tblGroupToTrip> tblGroupToTrips { get; set; }
        public virtual DbSet<tblReceipt> tblReceipts { get; set; }
        public virtual DbSet<tblTrip> ttblTrips { get; set; }
        public virtual DbSet<tblTripToUserExpense> tblTripToUserExpenses { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserExpense> tblUserExpenses { get; set; }
        public virtual DbSet<tblUserToGroup> tblUserToGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblGroup>()
                .Property(e => e.fldGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<tblGroup>()
                .HasMany(e => e.tblGroupToTrip)
                .WithOptional(e => e.tblGroup)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblGroup>()
                .HasMany(e => e.tblUserToGroup)
                .WithOptional(e => e.tblGroup)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblTrip>()
                .HasMany(e => e.tblGroupToTrip)
                .WithOptional(e => e.tblTrip)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblTrip>()
                .HasMany(e => e.tblTripToUserExpense)
                .WithOptional(e => e.tblTrip)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblUser>()
                .Property(e => e.fldEmail)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.fldFirstName)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.fldLastName)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .HasMany(e => e.tblUserToGroup)
                .WithOptional(e => e.tblUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblUserExpense>()
                .Property(e => e.fldNote)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserExpense>()
                .HasMany(e => e.tblTripToUserExpense)
                .WithOptional(e => e.tblUserExpense)
                .WillCascadeOnDelete();
        }
    }
}
