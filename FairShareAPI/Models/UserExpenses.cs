using System.ComponentModel.DataAnnotations;

namespace FairShareAPI.Models
{
	public class UserExpenses
	{

		public int Id { get; set; }
		[Required]
		public User UserId { get; set; }
		public double Expense { get; set; }
		public string Note { get; set; }
		public DateTime ExpenseDate { get; set; }

	}
}
