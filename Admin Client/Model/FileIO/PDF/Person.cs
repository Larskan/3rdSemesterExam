using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.FileIO.PDF
{
    public class Person
    {
        public int ID;
        public string FirstName; //fldFirstName in tblUser
        public string LastName; //fldLastName in tblUser
        public double Expenses; //fldExpense in tblUserExpense
        public double Sum; //fldSum in fldTrip
        public int Rest;// => (Expenses/Sum)*100;

        public Person(int id, string fname, string lname, int num, int sum)
        {       
            ID = id;
            FirstName = fname;
            LastName = lname;
            Expenses = num;
            Sum = sum;

        }
    }
}
