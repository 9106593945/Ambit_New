using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class cartitems : BaseEntity
	{
		[Key]
		public long id { get; set; }
		public long cartid { get; set; }
		public int itemid { get; set; }
		public int quantity { get; set; }
		public int customerloginid { get; set; }

	}
}
