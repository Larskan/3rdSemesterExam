//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLogin
    {
        public int fldLoginID { get; set; }
        public Nullable<int> fldUserID { get; set; }
        public string fldPassword { get; set; }
    
        public virtual tblUser tblUser { get; set; }
    }
}
