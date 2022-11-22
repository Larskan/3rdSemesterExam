using System.ComponentModel.DataAnnotations;

namespace FairShareAPI.Models
{
	public class Receipt
	{

		public string Id { get; set; }
		[Required]
		public User UserId { get; set; }
		[Required]
		public Trip TripId { get; set; }
		public double ProjectedValue { get; set; }
		public double AmountPayed { get; set; }

	}
}
