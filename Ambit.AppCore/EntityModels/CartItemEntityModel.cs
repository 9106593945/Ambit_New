namespace Ambit.AppCore.EntityModels
{
	public class CartItemEntityModel : BaseEntityModel
	{
          public long id { get; set; }
          public int cartid { get; set; }
          public int itemid { get; set; }
          public int quantity { get; set; }
          public bool Active { get; set; }
          public bool isDeleted { get; set; }
          public DateTime? Created_On { get; set; }
          public int Created_By { get; set; }
          public DateTime? Updated_On { get; set; }
          public int Updated_By { get; set; }
          public int customerloginid { get; set; }

         
     }
}
