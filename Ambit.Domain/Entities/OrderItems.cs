using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class OrderItems : BaseEntity
	{
		[Key]
		public long id { get; set; }
		public int orderid { get; set; }
		public int itemid { get; set; }
		public int quantity { get; set; }
		public decimal price { get; set; }
		public decimal tax { get; set; }
		public decimal total { get; set; }
		public int customerid { get; set; }
		public string customername { get; set; }

	}
}
