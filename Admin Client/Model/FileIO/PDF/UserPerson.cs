using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;

namespace Admin_Client.Model.FileIO.PDF
{
    public class UserPerson
    {
        public int ID;
        public string FirstName; //fldFirstName in tblUser
        public string LastName; //fldLastName in tblUser
        public double Expenses; //fldAmountPaid in tblUserExpense
        public double Sum; //fldSum in fldTrip

        public UserPerson(int id)
        {
            var userSelect = HttpClientHandler.GetUser(id);
            var expenseSelect = HttpClientHandler.GetUserExpensesFromUser(userSelect);
            var receiptSelect = HttpClientHandler.GetReceiptsFromUser(userSelect);

            ID = id;
            FirstName = userSelect.fldFirstName;
            LastName = userSelect.fldLastName;
            Expenses = 
            

        }
    }
}
