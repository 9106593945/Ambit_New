using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	[ApiController]
	public class RegisterController : ControllerBase
	{
		private readonly IUserService _userService;

		public RegisterController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public IActionResult CustomerUpsert([FromForm] RegisterRequestModel registerRequest)
		{
			var success = false;
			string Message = "";
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
	}
}
