using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class cart : BaseEntity
	{
		[Key]
		public long cartid { get; set; }
		public string customername { get; set; }
		public int customerloginid { get; set; }
		public int customerid { get; set; }
	}
}
