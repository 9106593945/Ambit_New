using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ambit.API.Controllers
{
    [ApiController]
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class BannerController : ControllerBase
	{
		private readonly IBannerService _bannerService;

		public BannerController(IBannerService bannerService)
		{
			_bannerService = bannerService;
		}

		[Route("Banners")]
		[HttpGet]
		public IActionResult GetBanners()
		{
			return Ok(_bannerService.GetAllBanners());
		}
	}
}
