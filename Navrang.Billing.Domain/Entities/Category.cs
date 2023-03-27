using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.Domain.Entities
{
	public class category : BaseEntity
	{
		[Key]
		public long categoryid { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string image { get; set; }
	}
}
