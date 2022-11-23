namespace FairShareAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tblLogin")]
    public partial class tblLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fldLoginID { get; set; }

        public int? fldUserID { get; set; }

        [StringLength(50)]
        public string fldPassword { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
