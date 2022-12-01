using Admin_Client.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{
	public class UserExpense
	{

		public int Id { get; set; }

		public string Name { get; set; }

		public string Amount { get; set; }

		public string Date { get; set; }

		public UserExpense(TblUserExpense userExpense) 
		{
			this.Id = 1;
			this.Name = "Expense 1";
			this.Amount = 100 + " DKK";
			this.Date = DateTime.Now.ToString();
		}

	}
}
