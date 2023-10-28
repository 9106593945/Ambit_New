using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.AppCore.Common
{
	public interface IUserService
	{
		UserApiModel Authenticate(string email, string password);
		UserApiModel GetCustomerLoginByUserName(string userName);
		ObjectResult RegisterCustomerLogin(RegisterRequestModel registerRequest);
		UserApiModel GetCustomerByUserName(string userName);
	}
}
