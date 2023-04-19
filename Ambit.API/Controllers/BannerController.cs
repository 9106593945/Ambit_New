using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	public class BannerController : BaseAPIController
	{
		private readonly IBannerService _bannerService;

		public BannerController(IBannerService bannerService)
		{
			_bannerService = bannerService;
		}

		[HttpGet]
		public IActionResult GetBanners()
		{
			CommonAPIReponse<IEnumerable<BannerEntityModel>> response = new()
			{
				data = _bannerService.GetAllBanners(),
				Message = "Banners get successfully",
				Success = true
			};
			return Ok(response);
		}
	}
}
