namespace Admin_Client.Model.DB.EF_Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblTripToUserExpense")]
    public partial class tblTripToUserExpense
    {
        [Key]
        public int fldTripToUserExpenseID { get; set; }

        public int fldTripID { get; set; }

        public int fldExpenseID { get; set; }

        public virtual tblTrip tblTrip { get; set; }

        public virtual tblUserExpense tblUserExpense { get; set; }
    }
}
