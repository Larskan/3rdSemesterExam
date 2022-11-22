using System.ComponentModel.DataAnnotations;

namespace FairShareAPI.Models
{
	public class User
	{

		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNum { get; set; }
		public ICollection<Group> Groups { get; set; }
		public ICollection<Receipt> Receipts { get; set; }
		public ICollection<UserExpenses> UserExpenses { get; set;}
	}
}
