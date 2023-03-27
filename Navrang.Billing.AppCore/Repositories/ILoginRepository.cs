using Microsoft.EntityFrameworkCore.ChangeTracking;
using Navrang.Billing.AppCore.EntityModels;
using Navrang.Billing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navrang.Billing.AppCore.Repositories
{
     public interface ILoginRepository
     {
          /// <summary>
          /// This will return a User api model object based on email and password.
          /// </summary>
          /// <param name="email"></param>
          /// <param name="password"></param>
          /// <returns></returns>
          UserApiModel GetLoginByEmailAndPasswordAndUpdateLastLoginTime(string email, string password);
          UserApiModel GetLoginById(long userId);
          UserApiModel GetCustomerLoginByUserName(string userName);
          EntityEntry<CustomerLogin> RegisterCustomer(RegisterRequestModel registerRequest);
          UserApiModel GetCustomerByUserName(string userName);
     }
}
