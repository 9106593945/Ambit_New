using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ambit.API.Controllers
{
	public class AccountController : BaseAPIController
	{
		private readonly ILogger<AccountController> _logger;
		private readonly IUserService _userService;
		private readonly AppSettings _appSettings;

		public AccountController(ILogger<AccountController> logger, IUserService userService,
			IOptions<AppSettings> appSettings)
		{
			_logger = logger;
			_userService = userService;
			_appSettings = appSettings.Value;
		}

		[AllowAnonymous]
		[Route("Authenticate")]
		[HttpPost]
		public IActionResult Post([FromBody] LoginRequestModel loginRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var user = _userService.GetCustomerLoginByUserName(loginRequest.UserName);
			string Message = "";
			_logger.LogInformation("Authenticate API calling :: ", loginRequest.UserName);

			if (user != null)
			{
				if (user.Password == loginRequest.Password && user.isApproved && (user.customerId == 0 || user.deviceId == loginRequest.DeviceId))
				{
					user.Token = GenerateJwtToken(user);
					user.Password = "";
					return Ok(new CommonAPIReponse<UserApiModel>()
					{
						Data = user,
						Message = Message,
						Status = 200
					});
				}
				else if (user.Password == loginRequest.Password && !user.isApproved)
				{
					Message = "Sorry! You are not authorized, Please contact to administrator.";
				}
				else if (user.Password == loginRequest.Password && user.isApproved && user.deviceId != loginRequest.DeviceId)
				{
					Message = "Please login from your Registerd device.";
				}
				else
				{
					Message = "Please enter your valid Password.";
				}
			}
			else
			{
				Message = "Please enter your valid Mobile Number.";
			}

			return Ok(new CommonAPIReponse<string>()
			{
				Message = Message,
				Status = 400
			});
		}

		private string GenerateJwtToken(UserApiModel user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] {
					new Claim("id", user.customerId.HasValue ? user.customerId.Value.ToString() : ""),
					new Claim(ClaimTypes.Sid, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Name.ToString()),
					new Claim(ClaimTypes.Role, "API")
				}),
				Expires = DateTime.UtcNow.AddHours(_appSettings.TokenExpireTime ?? 24),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
