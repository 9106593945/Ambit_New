namespace Ambit.AppCore.EntityModels
{
	public class ItemAPIEntityModel
	{
		public long? FavoriteItemId { get; set; }
		public long? ItemId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string ImagePath { get; set; }
		public string Code { get; set; }
		public decimal SellAmount { get; set; }
		public bool IsFavorite { get; set; }
	}
}
