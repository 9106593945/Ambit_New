namespace Ambit.AppCore.EntityModels
{
    public class BannerEntityModel
    {
        public long? BannerId { internal get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int? SortOrder { internal get; set; }

    }
}
