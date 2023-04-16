namespace Ambit.AppCore.EntityModels
{
	public class CategoryItemLinkEntityModel : BaseEntityModel
	{
		public long? CategoryItemLinkEntityModelId { get; set; }
		public string Name { get; set; }
		public long CategoryId { get; set; }
		public long ItemId { get; set; }
	}
}