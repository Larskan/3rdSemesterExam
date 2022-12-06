using System;
using System.Collections.Generic;
using FairShareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FairShareAPI.Data;

#pragma warning disable CS1591
public partial class FairShareDbContext : DbContext
{
    public FairShareDbContext()
    {
    }

    public FairShareDbContext(DbContextOptions<FairShareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tblGroup> tblGroups { get; set; }

    public virtual DbSet<tblGroupToTrip> tblGroupToTrips { get; set; }

    public virtual DbSet<tblLogin> tblLogins { get; set; }

    public virtual DbSet<tblReceipt> tblReceipts { get; set; }

    public virtual DbSet<tblTrip> tblTrips { get; set; }

    public virtual DbSet<tblTripToUserExpense> tblTripToUserExpenses { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    public virtual DbSet<tblUserExpense> tblUserExpenses { get; set; }

    public virtual DbSet<tblUserToGroup> tblUserToGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;database=FairShareDB;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblGroup>(entity =>
        {
            entity.HasKey(e => e.fldGroupId).HasName("PK__tblGroup__346DE83CB9DE401A");

            entity.ToTable("tblGroup");

            entity.Property(e => e.fldGroupId).HasColumnName("fldGroupID");
            entity.Property(e => e.fldGroupName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldGroupName");
            entity.Property(e => e.fldGroupBoolean).HasColumnName("fldGroupBoolean");
        });

        modelBuilder.Entity<tblGroupToTrip>(entity =>
        {
            entity.HasKey(e => e.fldGroupToTripId).HasName("PK__tblGroup__C215EEF18CA2C599");

            entity.ToTable("tblGroupToTrip");

            entity.Property(e => e.fldGroupToTripId).HasColumnName("fldGroupToTripID");
            entity.Property(e => e.fldGroupId).HasColumnName("fldGroupID");
            entity.Property(e => e.fldTripId).HasColumnName("fldTripID");

            entity.HasOne(d => d.fldGroup).WithMany(p => p.tblGroupToMoneys)
                .HasForeignKey(d => d.fldGroupId)
                .HasConstraintName("FK__tblGroupT__fldGr__30F848ED");

            entity.HasOne(d => d.fldTrip).WithMany(p => p.tblGroupToMoneys)
                .HasForeignKey(d => d.fldTripId)
                .HasConstraintName("FK__tblGroupT__fldTr__31EC6D26");
        });

        modelBuilder.Entity<tblLogin>(entity =>
        {
            entity.HasKey(e => e.fldLoginId).HasName("PK__tblLogin__5FFDE57FA6EADA6D");

            entity.ToTable("tblLogin");

            entity.Property(e => e.fldLoginId).HasColumnName("fldLoginID");
            entity.Property(e => e.fldPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fldPassword");
            entity.Property(e => e.fldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.fldUser).WithMany(p => p.tblLogins)
                .HasForeignKey(d => d.fldUserId)
                .HasConstraintName("FK__tblLogin__fldUse__2A4B4B5E");
        });

        modelBuilder.Entity<tblReceipt>(entity =>
        {
            entity.HasKey(e => e.fldReceiptId).HasName("PK__tblRecei__E502712E5AF2287A");

            entity.ToTable("tblReceipt");

            entity.Property(e => e.fldReceiptId).HasColumnName("fldReceiptID");
            entity.Property(e => e.fldAmountPaid).HasColumnName("fldAmountPaid");
            entity.Property(e => e.fldProjectedValue).HasColumnName("fldProjectedValue");
            entity.Property(e => e.fldTripId).HasColumnName("fldTripID");
            entity.Property(e => e.fldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.fldTrip).WithMany(p => p.tblReceipts)
                .HasForeignKey(d => d.fldTripId)
                .HasConstraintName("FK__tblReceip__fldTr__3C69FB99");

            entity.HasOne(d => d.fldUser).WithMany(p => p.tblReceipts)
                .HasForeignKey(d => d.fldUserId)
                .HasConstraintName("FK__tblReceip__fldUs__3B75D760");
        });

        modelBuilder.Entity<tblTrip>(entity =>
        {
            entity.HasKey(e => e.fldTripId).HasName("PK__tblTrip__8AEEDFA23CF9C1F2");

            entity.ToTable("tblTrip");

            entity.Property(e => e.fldTripId).HasColumnName("fldTripID");
            entity.Property(e => e.fldSum).HasColumnName("fldSum");
        });

        modelBuilder.Entity<tblTripToUserExpense>(entity =>
        {
            entity.HasKey(e => e.fldUserToExpense).HasName("PK__tblTripT__E9A07F6E6CCF54D8");

            entity.ToTable("tblTripToUserExpenses");

            entity.Property(e => e.fldUserToExpense).HasColumnName("fldUserToExpense");
            entity.Property(e => e.fldExpensesId).HasColumnName("fldExpensesID");
            entity.Property(e => e.fldTripId).HasColumnName("fldTripID");

            entity.HasOne(d => d.fldExpenses).WithMany(p => p.tblTripToUserExpenses)
                .HasForeignKey(d => d.fldExpensesId)
                .HasConstraintName("FK__tblTripTo__fldEx__38996AB5");

            entity.HasOne(d => d.fldTrip).WithMany(p => p.tblTripToUserExpenses)
                .HasForeignKey(d => d.fldTripId)
                .HasConstraintName("FK__tblTripTo__fldTr__37A5467C");
        });

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.fldUserId).HasName("PK__tblUser__DBF596A7CE2706D0");

            entity.ToTable("tblUser");

            entity.Property(e => e.fldUserId).HasColumnName("fldUserID");
            entity.Property(e => e.fldEmail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldEmail");
            entity.Property(e => e.fldFirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldFirstName");
            entity.Property(e => e.fldLastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldLastName");
            entity.Property(e => e.fldPhonenumber).HasColumnName("fldPhonenumber");
            entity.Property(e => e.fldIsAdmin).HasColumnName("fldIsAdmin");
        });

        modelBuilder.Entity<tblUserExpense>(entity =>
        {
            entity.HasKey(e => e.fldExpensesId).HasName("PK__tblUserE__8B84BEB85F4F3DA2");

            entity.ToTable("tblUserExpenses");

            entity.Property(e => e.fldExpensesId).HasColumnName("fldExpensesID");
            entity.Property(e => e.fldDate)
                .HasColumnType("date")
                .HasColumnName("fldDate");
            entity.Property(e => e.fldExpense).HasColumnName("fldExpense");
            entity.Property(e => e.fldNote)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fldNote");
            entity.Property(e => e.fldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.fldUser).WithMany(p => p.tblUserExpenses)
                .HasForeignKey(d => d.fldUserId)
                .HasConstraintName("FK__tblUserEx__fldUs__34C8D9D1");
        });

        modelBuilder.Entity<tblUserToGroup>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.fldUserToGroupId).HasName("PK__tblUserT__19DCF06B1673C2D9");
            //entity.HasAlternateKey(e => e.fldUserId);
            //entity.HasAlternateKey(e => e.fldGroupId);

            entity.ToTable("tblUserToGroup");

            entity.Property(e => e.fldUserToGroupId).HasColumnName("fldUserToGroupID");
            entity.Property(e => e.fldGroupId).HasColumnName("fldGroupID");
            entity.Property(e => e.fldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.fldGroup).WithMany(p => p.tblUserToGroups)
                .HasForeignKey(d => d.fldGroupId)
                .HasConstraintName("FK__tblUserTo__fldGr__2E1BDC42");

            entity.HasOne(d => d.fldUser).WithMany(p => p.tblUserToGroups)
                .HasForeignKey(d => d.fldUserId)
                .HasConstraintName("FK__tblUserTo__fldUs__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }
#pragma warning disable CS1591

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
