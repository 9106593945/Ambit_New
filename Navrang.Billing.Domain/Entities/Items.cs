using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.Domain.Entities
{
	public class Items : BaseEntity
	{
		[Key]
		public long itemid { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string supplier { get; set; }
		public string image { get; set; }
		public string code { get; set; }
		public decimal purchaseamount { get; set; }
		public decimal sellamount { get; set; }
		public decimal retailamount { get; set; }
		public decimal mrp { get; set; }
		public decimal discount { get; set; }
		public int openingquantity { get; set; }
		public bool showninapp { get; set; }
		public bool IsComboProduct { get; set; }
	}
}
