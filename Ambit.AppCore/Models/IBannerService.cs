using Ambit.AppCore.EntityModels;
using System.Collections.Generic;

namespace Ambit.AppCore.Models
{
	public interface IBannerService
	{
		IEnumerable<BannerEntityModel> GetAllBanners();
	}
}
