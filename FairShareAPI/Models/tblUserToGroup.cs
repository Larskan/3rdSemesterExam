namespace FairShareAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tblUserToGroup")]
    public partial class tblUserToGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fldUserToGroupID { get; set; }

        public int? fldUserID { get; set; }

        public int? fldGroupID { get; set; }

        public virtual tblGroup tblGroup { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
