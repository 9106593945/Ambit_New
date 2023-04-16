using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class CartEntityModel : BaseEntityModel
	{
		[Key]
		public int cartid { get; set; }
		public string customername { get; set; }
		public int customerloginid { get; set; }
		public virtual IEnumerable<CartItemEntityModel> CartItems { get; set; }
	}
}
