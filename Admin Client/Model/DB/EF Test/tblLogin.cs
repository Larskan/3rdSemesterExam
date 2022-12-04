namespace Admin_Client.Model.DB.EF_Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblLogin")]
    public partial class tblLogin
    {
        [Key]
        public int fldLoginID { get; set; }

        public int? fldUserID { get; set; }

        [StringLength(256)]
        public string fldPassword { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
