using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Repositories
{
	public interface IBannerRepository
	{
		IEnumerable<BannerEntityModel> GetAllBanners();
	}
}
