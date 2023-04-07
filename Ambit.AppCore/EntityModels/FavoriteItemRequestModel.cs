namespace Ambit.AppCore.EntityModels
{
    public class FavoriteItemRequestModel
	{
          public int itemId { get; set; }
          public int customerId { get; set; }
          public int customerLoginId { get; set; }
          public bool isFavorite { get; set; }
         
     }
}
