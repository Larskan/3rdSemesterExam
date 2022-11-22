using System.ComponentModel.DataAnnotations;

namespace FairShareAPI.Models
{
	public class Login
	{

		public string Id { get; set; }
		[Required]
		public User UserId { get; set; }
		public string Password { get; set; }

	}
}
