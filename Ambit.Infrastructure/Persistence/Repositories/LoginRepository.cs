using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ambit.Infrastructure.Persistence.Repositories
{
	public class LoginRepository : BaseRepository, ILoginRepository
	{
		public LoginRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public UserApiModel GetLoginByEmailAndPasswordAndUpdateLastLoginTime(string userName, string password)
		{
			var login = _dbContext.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
			if (login == null)
				return new UserApiModel();

			//Update login time...
			login.Last_Login = DateTime.Now;

			return GetUserApiModelFromLogin(login);
		}

		public UserApiModel GetCustomerLoginByUserName(string userName)
		{
			var login = _dbContext.CustomerLogin.Where(x => x.Username == userName && x.isDeleted == false).FirstOrDefault();
			if (login == null)
				return null;

			return new UserApiModel()
			{
				Id = login.Customerloginid,
				customerId = login.Customerid,
				Name = login.Name,
				isApproved = login.Isapproved,
				Password = login.Password,
				deviceId = login.Deviceid
			};
		}

		public UserApiModel GetLoginById(long userId)
		{
			var login = _dbContext.Users.Where(x => x.UserId == userId).FirstOrDefault();

			return GetUserApiModelFromLogin(login);
		}

		private UserApiModel GetUserApiModelFromLogin(Users login)
		{
			return new UserApiModel
			{
				Id = login.UserId,
				Email = login.Email,
				Username = login.UserName,
				Name = login.Name,
				Password = login.Password,
				Role = login.Role
			};
		}

		public EntityEntry<CustomerLogin> RegisterCustomer(int parentId, RegisterRequestModel registerRequest)
		{
			var CustomerLogin = _dbContext.CustomerLogin.Add(new CustomerLogin
			{
				Deviceid = registerRequest.DeviceId,
				Name = registerRequest.Name,
				Username = registerRequest.username,
				Password = registerRequest.Password,
				Invitationcode = registerRequest.InvitationCode,
				Type = registerRequest.Type,
				ParentId = parentId,
				Created_On = DateTime.UtcNow,
				Updated_On = DateTime.UtcNow,
				Active = true,
				Created_By = 0,
				Updated_By = 0
				//customerid = registerRequest.customerId
			});
			_dbContext.SaveChanges();
			return CustomerLogin;
		}

		public UserApiModel GetCustomerByUserName(string userName)
		{
			var login = _dbContext.Customer.Where(x => x.Name == userName).FirstOrDefault();
			if (login == null)
				return null;

			return new UserApiModel()
			{
				Id = login.customerid,
				Name = login.Name
			};
		}
	}
}
