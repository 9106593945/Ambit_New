using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
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
	public class AccountController : ControllerBase
	{
		private readonly ILogger<APIController> _logger;
		private readonly IUserService _userService;
		private readonly AppSettings _appSettings;

		public AccountController(ILogger<APIController> logger, IUserService userService,
			IOptions<AppSettings> appSettings)
		{
			_logger = logger;
			_userService = userService;
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

	}
}
