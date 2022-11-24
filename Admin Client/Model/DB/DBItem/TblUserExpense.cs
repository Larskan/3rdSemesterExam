using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Admin_Client.Model.DB
{
	public partial class TblUserExpense
	{
		public int FldExpensesId { get; set; }

		public int FldUserId { get; set; }

		public double FldExpense { get; set; }

		public string FldNote { get; set; }

		public DateTime FldDate { get; set; }

		[JsonIgnore]
		public virtual TblUser FldUser { get; set; }

		[JsonIgnore]
		public virtual ICollection<TblTripToUserExpense> TblTripToUserExpenses { get; } = new List<TblTripToUserExpense>();
	}

}