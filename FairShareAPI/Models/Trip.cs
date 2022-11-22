using System.ComponentModel.DataAnnotations;

namespace FairShareAPI.Models
{
	public class Trip
	{

		public string Id { get; set; }
		[Required]
		public string Title { get; set; }
		public double Sum { get; set; }
		public ICollection<UserExpenses> Expenses { get; set; }

	}
}
