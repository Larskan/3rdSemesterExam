namespace Admin_Client.Model.DB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblGroup")]
    public partial class tblGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblGroup()
        {
            tblGroupToTrip = new HashSet<tblGroupToTrip>();
            tblUserToGroup = new HashSet<tblUserToGroup>();
        }

        [Key]
        public int fldGroupID { get; set; }

        [StringLength(30)]
        public string fldGroupName { get; set; }

        public bool fldIsTemporary { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblGroupToTrip> tblGroupToTrip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserToGroup> tblUserToGroup { get; set; }
    }
}
