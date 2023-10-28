using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	public class RegisterController : BaseAPIController
	{
		private readonly IUserService _userService;

		public RegisterController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult Register([FromBody] RegisterRequestModel registerRequest)
		{
			if (ModelState.IsValid)
			{
				return _userService.RegisterCustomerLogin(registerRequest);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
