using Navrang.Billing.AppCore.EntityModels;

namespace Navrang.Billing.AppCore.Common
{
	public interface IUserService
	{
		UserApiModel Authenticate(string email, string password);
		UserApiModel GetCustomerLoginByUserName(string userName);
		bool RegisterCustomerLogin(RegisterRequestModel registerRequest);
		UserApiModel GetCustomerByUserName(string userName);
	}
}
