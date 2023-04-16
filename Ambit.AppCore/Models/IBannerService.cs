using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Models
{
	public interface IBannerService
	{
		IEnumerable<BannerEntityModel> GetAllBanners();
	}
}
