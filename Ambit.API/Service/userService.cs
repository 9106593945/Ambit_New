using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Ambit.Services
{
	public class UserService : IUserService
	{
		private readonly AppSettings _appSettings;
		private readonly IRepoSupervisor _repoSupervisor;
		public UserService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
		{
			_appSettings = appSettings.Value;
			_repoSupervisor = repoSupervisor;
		}

		public UserApiModel Authenticate(string email, string password)
		{
			var user = _repoSupervisor.Logins.GetLoginByEmailAndPasswordAndUpdateLastLoginTime(email, password);

			//Save changes;
			_repoSupervisor.Complete();
			return user;
		}

		public UserApiModel GetCustomerLoginByUserName(string userName)
		{
			var user = _repoSupervisor.Logins.GetCustomerLoginByUserName(userName);
			return user;
		}

		public ObjectResult RegisterCustomerLogin(RegisterRequestModel registerRequest)
		{
			try
			{
				var user = _repoSupervisor.Logins.GetCustomerLoginByUserName(registerRequest.username);
				if (user != null && user.Id > 0)
				{
					return Utils.GetObjectResult(400, new CommonAPIReponse<string>()
					{
						Error = "You are already registerd.",
						Status = 400,
						Message = "You are already registerd."
					});
				}

				long customerLoginId;
				if (_appSettings.DefaultInvitationCode == registerRequest.InvitationCode)
				{
					customerLoginId = 0;
					registerRequest.Type = nameof(CustomerType.Dealer);
				}
				else
				{
					registerRequest.Type = nameof(CustomerType.Customer);
					customerLoginId = _repoSupervisor.Customer.GetCustomerIdFromInvitationCode(registerRequest.InvitationCode);

					if (customerLoginId == 0)
					{
						return Utils.GetObjectResult(400, new CommonAPIReponse<string>()
						{
							Error = "Invalid Invitation Code.",
							Status = 400,
							Message = "Invalid Invitation Code."
						});
					}
				}
				do
				{
					registerRequest.InvitationCode = ExtensionMethods.GenerateRandomOtp(6);
				} while (_repoSupervisor.Customer.GetCustomerIdFromInvitationCode(registerRequest.InvitationCode) > 0);

				var customerLogin = _repoSupervisor.Logins.RegisterCustomer(Convert.ToInt16(customerLoginId), registerRequest);

				if (customerLogin.Entity.Customerloginid > 0)
					return Utils.GetObjectResult(200, new CommonAPIReponse<string>()
					{
						Message = "Customer registered successfully.",
						Status = 200
					});

				return Utils.GetObjectResult(200, new CommonAPIReponse<string>()
				{
					Message = "Customer not registered. Contact administrator.",
					Status = 400
				});

			}
			catch (Exception e)
			{
				return Utils.GetObjectResult(200, new CommonAPIReponse<string>()
				{
					Message = "Invalid request.",
					Status = 400,
					Error = e.Message + " : " + e.StackTrace + " " + e.InnerException
				}); ;
			}
		}

		public UserApiModel GetCustomerByUserName(string userName)
		{
			return _repoSupervisor.Logins.GetCustomerByUserName(userName);
		}
	}
}
