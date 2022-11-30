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

    public virtual DbSet<TblGroup> TblGroups { get; set; }

    public virtual DbSet<TblGroupToMoney> TblGroupToMoneys { get; set; }

    public virtual DbSet<TblLogin> TblLogins { get; set; }

    public virtual DbSet<TblReceipt> TblReceipts { get; set; }

    public virtual DbSet<TblTrip> TblTrips { get; set; }

    public virtual DbSet<TblTripToUserExpense> TblTripToUserExpenses { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserExpense> TblUserExpenses { get; set; }

    public virtual DbSet<TblUserToGroup> TblUserToGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;database=FairShareDB;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblGroup>(entity =>
        {
            entity.HasKey(e => e.FldGroupId).HasName("PK__tblGroup__346DE83CB9DE401A");

            entity.ToTable("tblGroup");

            entity.Property(e => e.FldGroupId).HasColumnName("fldGroupID");
            entity.Property(e => e.FldGroupName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldGroupName");
            entity.Property(e => e.FldGroupBoolean).HasColumnName("fldGroupBoolean");
        });

        modelBuilder.Entity<TblGroupToMoney>(entity =>
        {
            entity.HasKey(e => e.FldGroupToMoneyId).HasName("PK__tblGroup__C215EEF18CA2C599");

            entity.ToTable("tblGroupToMoney");

            entity.Property(e => e.FldGroupToMoneyId).HasColumnName("fldGroupToMoneyID");
            entity.Property(e => e.FldGroupId).HasColumnName("fldGroupID");
            entity.Property(e => e.FldTripId).HasColumnName("fldTripID");

            entity.HasOne(d => d.FldGroup).WithMany(p => p.TblGroupToMoneys)
                .HasForeignKey(d => d.FldGroupId)
                .HasConstraintName("FK__tblGroupT__fldGr__30F848ED");

            entity.HasOne(d => d.FldTrip).WithMany(p => p.TblGroupToMoneys)
                .HasForeignKey(d => d.FldTripId)
                .HasConstraintName("FK__tblGroupT__fldTr__31EC6D26");
        });

        modelBuilder.Entity<TblLogin>(entity =>
        {
            entity.HasKey(e => e.FldLoginId).HasName("PK__tblLogin__5FFDE57FA6EADA6D");

            entity.ToTable("tblLogin");

            entity.Property(e => e.FldLoginId).HasColumnName("fldLoginID");
            entity.Property(e => e.FldPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fldPassword");
            entity.Property(e => e.FldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.FldUser).WithMany(p => p.TblLogins)
                .HasForeignKey(d => d.FldUserId)
                .HasConstraintName("FK__tblLogin__fldUse__2A4B4B5E");
        });

        modelBuilder.Entity<TblReceipt>(entity =>
        {
            entity.HasKey(e => e.FldReceiptId).HasName("PK__tblRecei__E502712E5AF2287A");

            entity.ToTable("tblReceipt");

            entity.Property(e => e.FldReceiptId).HasColumnName("fldReceiptID");
            entity.Property(e => e.FldAmountPaid).HasColumnName("fldAmountPaid");
            entity.Property(e => e.FldProjectedValue).HasColumnName("fldProjectedValue");
            entity.Property(e => e.FldTripId).HasColumnName("fldTripID");
            entity.Property(e => e.FldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.FldTrip).WithMany(p => p.TblReceipts)
                .HasForeignKey(d => d.FldTripId)
                .HasConstraintName("FK__tblReceip__fldTr__3C69FB99");

            entity.HasOne(d => d.FldUser).WithMany(p => p.TblReceipts)
                .HasForeignKey(d => d.FldUserId)
                .HasConstraintName("FK__tblReceip__fldUs__3B75D760");
        });

        modelBuilder.Entity<TblTrip>(entity =>
        {
            entity.HasKey(e => e.FldTripId).HasName("PK__tblTrip__8AEEDFA23CF9C1F2");

            entity.ToTable("tblTrip");

            entity.Property(e => e.FldTripId).HasColumnName("fldTripID");
            entity.Property(e => e.FldSum).HasColumnName("fldSum");
        });

        modelBuilder.Entity<TblTripToUserExpense>(entity =>
        {
            entity.HasKey(e => e.FldUserToExpense).HasName("PK__tblTripT__E9A07F6E6CCF54D8");

            entity.ToTable("tblTripToUserExpenses");

            entity.Property(e => e.FldUserToExpense).HasColumnName("fldUserToExpense");
            entity.Property(e => e.FldExpensesId).HasColumnName("fldExpensesID");
            entity.Property(e => e.FldTripId).HasColumnName("fldTripID");

            entity.HasOne(d => d.FldExpenses).WithMany(p => p.TblTripToUserExpenses)
                .HasForeignKey(d => d.FldExpensesId)
                .HasConstraintName("FK__tblTripTo__fldEx__38996AB5");

            entity.HasOne(d => d.FldTrip).WithMany(p => p.TblTripToUserExpenses)
                .HasForeignKey(d => d.FldTripId)
                .HasConstraintName("FK__tblTripTo__fldTr__37A5467C");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.FldUserId).HasName("PK__tblUser__DBF596A7CE2706D0");

            entity.ToTable("tblUser");

            entity.Property(e => e.FldUserId).HasColumnName("fldUserID");
            entity.Property(e => e.FldEmail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldEmail");
            entity.Property(e => e.FldFirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldFirstName");
            entity.Property(e => e.FldLastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fldLastName");
            entity.Property(e => e.FldPhonenumber).HasColumnName("fldPhonenumber");
            entity.Property(e => e.FldIsAdmin).HasColumnName("fldIsAdmin");
        });

        modelBuilder.Entity<TblUserExpense>(entity =>
        {
            entity.HasKey(e => e.FldExpensesId).HasName("PK__tblUserE__8B84BEB85F4F3DA2");

            entity.ToTable("tblUserExpenses");

            entity.Property(e => e.FldExpensesId).HasColumnName("fldExpensesID");
            entity.Property(e => e.FldDate)
                .HasColumnType("date")
                .HasColumnName("fldDate");
            entity.Property(e => e.FldExpense).HasColumnName("fldExpense");
            entity.Property(e => e.FldNote)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fldNote");
            entity.Property(e => e.FldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.FldUser).WithMany(p => p.TblUserExpenses)
                .HasForeignKey(d => d.FldUserId)
                .HasConstraintName("FK__tblUserEx__fldUs__34C8D9D1");
        });

        modelBuilder.Entity<TblUserToGroup>(entity =>
        {
            entity.HasKey(e => e.FldUserToGroupId).HasName("PK__tblUserT__19DCF06B1673C2D9");

            entity.ToTable("tblUserToGroup");

            entity.Property(e => e.FldUserToGroupId).HasColumnName("fldUserToGroupID");
            entity.Property(e => e.FldGroupId).HasColumnName("fldGroupID");
            entity.Property(e => e.FldUserId).HasColumnName("fldUserID");

            entity.HasOne(d => d.FldGroup).WithMany(p => p.TblUserToGroups)
                .HasForeignKey(d => d.FldGroupId)
                .HasConstraintName("FK__tblUserTo__fldGr__2E1BDC42");

            entity.HasOne(d => d.FldUser).WithMany(p => p.TblUserToGroups)
                .HasForeignKey(d => d.FldUserId)
                .HasConstraintName("FK__tblUserTo__fldUs__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }
#pragma warning disable CS1591

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
