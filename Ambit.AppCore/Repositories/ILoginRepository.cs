﻿using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ambit.AppCore.Repositories
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
		EntityEntry<CustomerLogin> RegisterCustomer(int parentid, RegisterRequestModel registerRequest);
		UserApiModel GetCustomerByUserName(string userName);
	}
}
