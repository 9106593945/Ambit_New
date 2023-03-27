using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.Domain.Entities
{
	public class cartitems : BaseEntity
	{
		[Key]
		public long cartitemid { get; set; }
		public long cartid { get; set; }
		public long itemid { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string image { get; set; }
		public int quantity { get; set; }
		public decimal price { get; set; }
		public decimal totalamount { get; set; }
		public decimal discount { get; set; }
	}
}
