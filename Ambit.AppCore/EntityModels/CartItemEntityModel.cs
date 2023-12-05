namespace Ambit.AppCore.EntityModels
{
	public class CartItemEntityModel : BaseEntityModel
	{
		public long Id { get; set; }
		public int CartId { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public int CustomerLoginId { get; set; }
	}
}
