namespace Ambit.AppCore.EntityModels
{
	public class CartItemEntityModel : BaseEntityModel
	{
          public long ItemId { get; set; }
          public long CartId { get; set; }
          public long CartItemId { get; set; }
          public string Name { get; set; }
          public string Image { get; set; }
          public string ImagePath { get; set; }
          public string Code { get; set; }
          public int Quantity { get; set; }
          public decimal SellingPrice { get; set; }
          public decimal Amount { get; set; }
         
     }
}
