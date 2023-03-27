using Navrang.Billing.AppCore.EntityModels;
using System.Collections.Generic;

namespace Navrang.Billing.AppCore.Models
{
	public interface IBannerService
	{
		IEnumerable<BannerEntityModel> GetAllBanners();
	}
}
