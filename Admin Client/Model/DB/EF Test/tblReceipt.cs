namespace Admin_Client.Model.DB.EF_Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblReceipt")]
    public partial class tblReceipt
    {
        [Key]
        public int fldReceiptID { get; set; }

        public int fldUserID { get; set; }

        public int fldTripID { get; set; }

        public double fldProjectedValue { get; set; }

        public double fldAmountPaid { get; set; }

        public virtual tblTrip tblTrip { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
