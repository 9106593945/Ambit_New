namespace Ambit.AppCore.EntityModels
{
	public class CartItemEntityModel : BaseEntityModel
	{
		public long Id { get; set; }
		public long CartId { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public int CustomerLoginId { get; set; }
		public int CustomerId { get; set; }
	}

	public class CartItemRequest
	{
		public long Id { get; set; }
		public long CartId { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public int CustomerLoginId { get; set; }
	}
}
