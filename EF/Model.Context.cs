﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FairShareDBEntities : DbContext
    {
        public FairShareDBEntities()
            : base("name=FairShareDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
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
