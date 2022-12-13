using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.FileIO.TemplatePDF
{
    public class TemplateUser
    {
        public string FirstName;
        public string LastName;
        public int Cost;
        public int Amount;
        public int Total => Cost * Amount;

        public TemplateUser(string fname, string lname, int num, int quan)
        {
            FirstName = fname;
            LastName = lname;
            Cost = num;
            Amount = quan;

            //Id = tbluser.fldUserID;
            //Mail = tbluser.fldEmail;
            // FirstName = tbluser.fldFirstName;
            // LastName = tbluser.fldLastName;
            //Number = tbluser.fldPhonenumber;
        }
    }
}

