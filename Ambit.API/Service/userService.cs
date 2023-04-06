using Microsoft.Extensions.Options;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Common;
using Ambit.API.Helpers;

namespace Navrang.Services
{
	public class userService : IUserService
     {
          private readonly AppSettings _appSettings;
          private readonly IRepoSupervisor _repoSupervisor;
          public userService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
          {
               _appSettings = appSettings.Value;
               _repoSupervisor = repoSupervisor;
          }

          public UserApiModel Authenticate(string email, string password)
          {
               var user = _repoSupervisor.Logins.GetLoginByEmailAndPasswordAndUpdateLastLoginTime(email, password);

               //Save changes;
               _repoSupervisor.Complete();

               if (user == null)
                    return null;

               return user;
          }

          public UserApiModel GetCustomerLoginByUserName(string userName)
          {
               var user = _repoSupervisor.Logins.GetCustomerLoginByUserName(userName);

               if (user == null)
                    return null;

               return user;
          }

          public bool RegisterCustomerLogin(RegisterRequestModel registerRequest)
          {
               var customerLogin = _repoSupervisor.Logins.RegisterCustomer(registerRequest);
               _repoSupervisor.Complete();

               if (customerLogin.Entity.customerloginid > 0)
                    return true;

               return false;
          }

          public UserApiModel GetCustomerByUserName(string userName) {

               return _repoSupervisor.Logins.GetCustomerByUserName(userName);
          }
     }
}
