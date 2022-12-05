namespace Admin_Client.Model.DB.EF_Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUser")]
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            tblLogin = new HashSet<tblLogin>();
            tblReceipt = new HashSet<tblReceipt>();
            tblUserExpense = new HashSet<tblUserExpense>();
            tblUserToGroup = new HashSet<tblUserToGroup>();
        }

        [Key]
        public int fldUserID { get; set; }

        [StringLength(30)]
        public string fldEmail { get; set; }

        [StringLength(30)]
        public string fldFirstName { get; set; }

        [StringLength(30)]
        public string fldLastName { get; set; }

        public int? fldPhonenumber { get; set; }

        public bool? fldIsAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLogin> tblLogin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblReceipt> tblReceipt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserExpense> tblUserExpense { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserToGroup> tblUserToGroup { get; set; }
    }
}
