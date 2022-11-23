namespace FairShareAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tblTripToUserExpenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fldUserToExpense { get; set; }

        public int? fldTripID { get; set; }

        public int? fldExpensesID { get; set; }

        public virtual tblTrip tblTrip { get; set; }

        public virtual tblUserExpenses tblUserExpenses { get; set; }
    }
}
