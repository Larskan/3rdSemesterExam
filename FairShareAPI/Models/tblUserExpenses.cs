namespace FairShareAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tblUserExpenses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUserExpenses()
        {
            tblTripToUserExpenses = new HashSet<tblTripToUserExpenses>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fldExpensesID { get; set; }

        public int? fldUserID { get; set; }

        public double? fldExpense { get; set; }

        [StringLength(100)]
        public string fldNote { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fldDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTripToUserExpenses> tblTripToUserExpenses { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
