//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUserExpenses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUserExpenses()
        {
            this.tblTripToUserExpenses = new HashSet<tblTripToUserExpenses>();
        }
    
        public int fldExpensesID { get; set; }
        public Nullable<int> fldUserID { get; set; }
        public Nullable<double> fldExpense { get; set; }
        public string fldNote { get; set; }
        public Nullable<System.DateTime> fldDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTripToUserExpenses> tblTripToUserExpenses { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
