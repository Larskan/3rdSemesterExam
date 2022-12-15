namespace Admin_Client.Model.DB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUserToGroup")]
    public partial class tblUserToGroup
    {
        [Key]
        public int fldUserToGroupID { get; set; }

        public int fldUserID { get; set; }

        public int fldGroupID { get; set; }

        public virtual tblGroup tblGroup { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
