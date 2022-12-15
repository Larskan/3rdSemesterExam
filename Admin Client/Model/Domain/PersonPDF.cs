using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;

namespace Admin_Client.Model.Domain {

    /// <summary>
    /// <para>The details chosen in the Receipt</para>
    /// </summary>
    public class PersonPDF
    {
        public int ID;
        public string FirstName; //fldFirstName in tblUser
        public string LastName; //fldLastName in tblUser
        public string Note; //fldNote in tblTrip
        public double Expenses; //fldAmountPaid in tblUserExpense
        public double Total; //fldSum in tblTrip

        public PersonPDF()
        {
        }

    }

}
