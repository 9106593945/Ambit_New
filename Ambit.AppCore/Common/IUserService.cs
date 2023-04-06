using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Common
{
	public interface IUserService
	{
		UserApiModel Authenticate(string email, string password);
		UserApiModel GetCustomerLoginByUserName(string userName);
		bool RegisterCustomerLogin(RegisterRequestModel registerRequest);
		UserApiModel GetCustomerByUserName(string userName);
	}
}
