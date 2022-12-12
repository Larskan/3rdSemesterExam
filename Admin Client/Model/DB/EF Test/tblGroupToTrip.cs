namespace Admin_Client.Model.DB.EF_Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblGroupToTrip")]
    public partial class tblGroupToTrip
    {
        [Key]
        public int fldGroupToTripID { get; set; }

        public int fldGroupID { get; set; }

        public int fldTripID { get; set; }

        public virtual tblGroup tblGroup { get; set; }

        public virtual tblTrip tblTrip { get; set; }
    }
}
