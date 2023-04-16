using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace Ambit.Infrastructure.Persistence.Repositories
{
	public class CustomerRepository : BaseRepository, ICustomerRepository
	{
		private readonly IDapper _dapper;

		public CustomerRepository(AppDbContext dbContext, IDapper dapper) : base(dbContext)
		{
			_dapper = dapper;
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public IEnumerable<CustomerEntityModel> AllCustomers()
		{
			return _dbContext.Customer.Where(a => a.isDeleted == false)
					.Select(x => new CustomerEntityModel()
					{
						Customer_Number = x.customer_number,
						Customerid = x.customerid,
						Name = x.Name,
					});
		}

		public IEnumerable<CustomerEntityModel> GetAllCustomers()
		{
			var Customers = _dapper.GetAll<CustomerEntityModel>($"exec [GetAllCustomers]", null, commandType: CommandType.Text);

			if (Customers == null)
				return null;
			return Customers;
		}

		public CustomerEntityModel GetCustomerById(int Id)
		{
			var Customers = _dbContext.Customer.Where(a => a.isDeleted == false && a.customerid == Id)
				.Select(x => new CustomerEntityModel()
				{
					Customer_Number = x.customer_number,
					Customerid = x.customerid,
					Name = x.Name,
					Mobile = x.mobile,
					Address = x.address,
					City = x.city,
					Country = x.country,
					Email = x.email,
					PostCode = x.postcode,
					State = x.state,
					//Password = x.password,
					Active = x.Active,
					Created_By = x.Created_By,
					Created_On = x.Created_On,
					Description = x.description,
					Transport = x.transport
				}).FirstOrDefault();

			if (Customers == null)
				return null;

			return Customers;
		}

		public EntityEntry<Customer> AddNewCustomer(CustomerEntityModel CustomerEntityModel)
		{
			var customer = _dbContext.Customer.Add(new Customer
			{
				customer_number = CustomerEntityModel.Customer_Number,
				Name = CustomerEntityModel.Name,
				mobile = CustomerEntityModel.Mobile,
				email = CustomerEntityModel.Email,
				postcode = CustomerEntityModel.PostCode,
				country = CustomerEntityModel.Country,
				state = CustomerEntityModel.State,
				address = CustomerEntityModel.Address,
				city = CustomerEntityModel.City,
				//password = CustomerEntityModel.Password,
				description = CustomerEntityModel.Description,
				transport = CustomerEntityModel.Transport
			});

			return customer;
		}

		public bool UpdateCustomerNumber(long customerId, string customerNumber)
		{
			var customer = _dbContext.Customer.Find(customerId);
			if (customer != null)
			{
				customer.customer_number = customerNumber;
			}
			return true;
		}

		public bool UpdateCustomer(CustomerEntityModel CustomerEntityModel)
		{
			var Customer = _dbContext.Customer.Find(CustomerEntityModel.Customerid);
			if (Customer != null)
			{
				Customer.Name = CustomerEntityModel.Name;
				Customer.mobile = CustomerEntityModel.Mobile;
				Customer.email = CustomerEntityModel.Email;
				Customer.postcode = CustomerEntityModel.PostCode;
				Customer.country = CustomerEntityModel.Country;
				Customer.state = CustomerEntityModel.State;
				Customer.address = CustomerEntityModel.Address;
				Customer.city = CustomerEntityModel.City;
				//Customer.password = CustomerEntityModel.Password;
				Customer.description = CustomerEntityModel.Description;
				Customer.transport = CustomerEntityModel.Transport;
				return true;
			}
			return false;
		}

		public bool IsCustomerCodeExist(string CustomerCode)
		{
			var Customer = _dbContext.Customer.Where(i => i.customer_number.ToUpper() == CustomerCode.ToUpper() && i.isDeleted == false);
			if (Customer != null && Customer.Count() > 0)
			{
				return true;
			}
			return false;
		}

		public CustomerEntityModel GetCustomerDetailByName(string CustomerName)
		{
			var Customer = _dbContext.Customer
				.Where(i => i.Name.ToUpper() == CustomerName.ToUpper() && i.isDeleted == false)
				.Select(s => new CustomerEntityModel()
				{
					Name = s.Name,
					Customerid = s.customerid,
					Customer_Number = s.customer_number
				});
			return Customer.FirstOrDefault();
		}

		public bool DeleteCustomer(long id)
		{
			var Customer = _dbContext.Customer.Find(id);
			if (Customer != null)
			{
				Customer.isDeleted = true;
				return true;
			}
			return false;
		}

		public bool ActiveInactiveCustomer(long id, bool status)
		{
			var Customer = _dbContext.Customer.Find(id);
			if (Customer != null)
			{
				Customer.Active = status;
				return true;
			}
			return false;
		}

		public List<CustomerEntityModel> GetCustomersByKey(string key)
		{
			var Customer = _dbContext.Customer.Where(i => (i.Name.Contains(key) || i.customer_number.Contains(key)) && i.isDeleted == false && i.Active == true)
				.Select(x => new CustomerEntityModel()
				{
					Customer_Number = x.customer_number,
					Customerid = x.customerid,
					Name = x.Name,
					Mobile = x.mobile,
					Address = x.address,
					City = x.city,
					State = x.state,
				}).ToList();
			return Customer;
		}

		public IEnumerable<CustomerLoginModel> GetAllCustomerLogin()
		{
			var customerLogin = _dbContext.ViewAllCustomerLogin.Select(s => new CustomerLoginModel()
			{
				Customer_Number = s.customer_number,
				Customerid = s.customerid,
				Customer_Name = s.customername,
				UserName = s.username,
				CustomerLoginid = s.customerloginid,
				Password = s.password,
				Name = s.name,
				Active = s.Active,
				Created_By = s.Created_By,
				Created_On = s.Created_On,
				deviceId = s.deviceid,
				isApproved = s.isapproved,
			});

			if (customerLogin == null)
				return null;
			return customerLogin;
		}

		public bool ActiveInactiveCustomerLogin(long id, bool status)
		{
			var Customer = _dbContext.CustomerLogin.Find(id);
			if (Customer != null)
			{
				Customer.Active = status;
				return true;
			}
			return false;
		}

		public bool ApprovedCustomerLogin(long id, bool status)
		{
			var Customer = _dbContext.CustomerLogin.Find(id);
			if (Customer != null)
			{
				Customer.isapproved = status;
				return true;
			}
			return false;
		}

		public bool UpdateCustomerToLogin(long id, long customerId, string name, string userName, string Password)
		{
			var Customer = _dbContext.CustomerLogin.Find(id);
			if (Customer != null)
			{
				Customer.customerid = customerId;
				Customer.username = userName ?? Customer.username;
				Customer.password = Password ?? Customer.password;
				Customer.name = name ?? Customer.name;
				return true;
			}
			return false;
		}
	}
}
