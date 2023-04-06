using Ambit.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ambit.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class APIController : ControllerBase
	{
		private readonly ILogger<APIController> _logger;
		private readonly IUserService _userService;
		private readonly IitemService _itemService;
		private readonly AppSettings _appSettings;
		private readonly IBannerService _bannerService;

		public APIController(ILogger<APIController> logger, IUserService userService,
			IOptions<AppSettings> appSettings, IitemService itemService, IBannerService bannerService)
		{
			_logger = logger;
			_userService = userService;
			_itemService = itemService;
			_bannerService = bannerService;
			_appSettings = appSettings.Value;
		}

		[AllowAnonymous]
		[Route("Authenticate")]
		[HttpPost]
		public IActionResult Post([FromForm] LoginRequestModel loginRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var user = _userService.GetCustomerLoginByUserName(loginRequest.userName);

			var authenticated = false; string Message = "";
			_logger.LogInformation("Authenticate API calling :: ", loginRequest.userName);
			if (user != null)
			{
				if (user.Password == loginRequest.password && user.isApproved && (user.customerId == 0 || user.deviceId == loginRequest.deviceid))
				{
					user.Token = generateJwtToken(user);
					authenticated = true;
				}
				else if (user.Password == loginRequest.password && !user.isApproved)
				{
					authenticated = false;
					Message = "Sorry! You are not authorized, Please contact to administrator.";
				}
				else if (user.Password == loginRequest.password && user.isApproved && user.deviceId != loginRequest.deviceid)
				{
					authenticated = false;
					Message = "Please login from your Registerd device.";
				}
				else
				{
					authenticated = false;
					Message = "Please enter your valid Password.";
				}
			}
			else
			{
				authenticated = false;
				Message = "Please enter your valid Mobile Number.";

			}

			if (authenticated)
			{
				user.Password = "";
				return Ok(new CommonAPIReponse<UserApiModel>()
				{
					data = user,
					Message = Message,
					Success = authenticated
				});
			}
			return Ok(new CommonAPIReponse<string>()
			{
				Message = Message,
				Success = authenticated
			});
		}

		private string generateJwtToken(UserApiModel user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] {
					new Claim("id", user.customerId.HasValue ? user.customerId.Value.ToString() : ""),
					new Claim(ClaimTypes.Sid, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, "API")
				}),
				Expires = DateTime.UtcNow.AddHours(_appSettings.TokenExpireTime ?? 24),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		[AllowAnonymous]
		[Route("Register")]
		[HttpPost]
		public IActionResult Register([FromForm] RegisterRequestModel registerRequest)
		{
			var success = false; string Message = "";
			if (ModelState.IsValid)
			{
				var user = _userService.GetCustomerLoginByUserName(registerRequest.username);
				if (user != null && user.Id > 0)
				{
					success = false;
					Message = "You are already registerd.";
				}
				else
				{
					var customer = _userService.GetCustomerByUserName(registerRequest.username);
					if (customer != null && customer.Id > 0)
					{
						registerRequest.customerId = customer.Id;
					}
					var customerReg = _userService.RegisterCustomerLogin(registerRequest);
					if (customerReg)
					{
						success = true;
						Message = "Your Registration has been successfully received.";
					}
				}

				return Ok(new CommonAPIReponse<string>()
				{
					data = "",
					Message = Message,
					Success = success
				});
			}
			else
			{
				return BadRequest();
			}
		}


		[Route("GetAllFavorite")]
		[HttpPost]
		public IActionResult GetAllFavorite([FromForm] int customerId, [FromForm] int customerLoginId)
		{
			IEnumerable<ItemEntityModel> favoriteItems = _itemService.GetAllFavoriteItem(customerLoginId);
			var response = new List<ItemAPIEntityModel>();
			response = favoriteItems.Select(s => new ItemAPIEntityModel()
			{
				Code = s.Code,
				Image = s.Image,
				ImagePath = s.ImagePath,
				Description = s.Description,
				FavoriteItemId = s.favoriteitemId,
				ItemId = s.ItemId,
				Name = s.Name,
				IsFavorite = s.IsFavorite,
				SellAmount = s.SellAmount
			}).ToList();

			return Ok(response);
		}

		[Route("ClearAllFavorite")]
		[HttpPost]
		public IActionResult ClearAllFavorite([FromForm] int customerId, [FromForm] int customerLoginId)
		{
			bool favoriteItems = _itemService.ClearAllFavorite(customerLoginId);
			return Ok(new JObject { { "message", "Favorite Items clear successfully." } });
		}

		[Route("UpsertFavoriteItem")]
		[HttpPost]
		public IActionResult UpsertFavoriteItem([FromForm] int itemId, [FromForm] int customerId, [FromForm] int customerLoginId, [FromForm] bool isFavorite)
		{
			bool favoriteItem = _itemService.UpsertFavoriteItem(customerLoginId, itemId, isFavorite);
			if (favoriteItem)
			{
				IEnumerable<ItemAPIEntityModel> favoriteItems = _itemService.GetAllFavoriteItem(customerLoginId)
					.Select(s => new ItemAPIEntityModel()
					{
						Code = s.Code,
						Image = s.Image,
						ImagePath = s.ImagePath,
						Description = s.Description,
						FavoriteItemId = s.favoriteitemId,
						ItemId = s.ItemId,
						Name = s.Name,
						IsFavorite = s.IsFavorite,
						SellAmount = s.SellAmount
					});

				return Ok(favoriteItems);
			}

			return BadRequest();
		}

		[Route("Banners")]
		[HttpGet]
		public IActionResult GetBanners()
		{
			var banners = _bannerService.GetAllBanners();

			return Ok(banners);
		}
	}
}
