namespace Admin_Client.Model.DB.EF_Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUserExpense")]
    public partial class tblUserExpense
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUserExpense()
        {
            tblTripToUserExpense = new HashSet<tblTripToUserExpense>();
        }

        [Key]
        public int fldExpenseID { get; set; }

        public int? fldUserID { get; set; }

        public double? fldExpense { get; set; }

        [StringLength(100)]
        public string fldNote { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fldDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTripToUserExpense> tblTripToUserExpense { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
