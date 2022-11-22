using System.ComponentModel.DataAnnotations;

namespace FairShareAPI.Models
{
	public class Group
	{

		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public bool IsTemp { get; set; }
		public ICollection<User> Users { get; set; }
		public ICollection<Trip> Trips { get; set; }

	}
}
