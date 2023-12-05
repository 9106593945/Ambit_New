namespace Ambit.AppCore.EntityModels
{
	public class BaseEntityModel
	{
		public bool? Active { get; set; }
		public bool? IsDeleted { get; set; } = false;
		public DateTime? Created_On { get; set; }
		public long? Created_By { get; set; }
		public DateTime? Updated_On { get; set; }
		public long? Updated_By { get; set; }
	}
}
