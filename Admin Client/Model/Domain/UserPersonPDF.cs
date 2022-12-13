using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;

namespace Admin_Client.Model.Domain { 

    public class UserPersonPDF
    {
        public int ID;
        public string FirstName; //fldFirstName in tblUser
        public string LastName; //fldLastName in tblUser
        public string TripName;
        public double Expenses; //fldAmountPaid in tblUserExpense
        public double Total; //fldSum in fldTrip

        public UserPersonPDF()
        {
        }

    }

}
