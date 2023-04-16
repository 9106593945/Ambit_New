using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.Extensions.Options;

namespace Ambit.Services
{
	public class customerService : ICustomerService
	{
		private readonly AppSettings _appSettings;
		private readonly IRepoSupervisor _repoSupervisor;
		public customerService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
		{
			_appSettings = appSettings.Value;
			_repoSupervisor = repoSupervisor;
		}

		public IEnumerable<CustomerEntityModel> GetCustomerList()
		{
			return _repoSupervisor.Customer.GetAllCustomers();
		}


		public CustomerEntityModel GetCustomerByID(int Id)
		{
			return _repoSupervisor.Customer.GetCustomerById(Id);
		}

		public long AddNewCustomer(CustomerEntityModel CustomerEntityModel)
		{
			try
			{
				var customerId = IsCustomerExist(CustomerEntityModel.Name);
				if (customerId > 0)
				{
					return customerId;
				}

				//Mobile - ISD Code
				CustomerEntityModel.Mobile = new Common().AddMobileISDCode(CustomerEntityModel.Mobile);

				var customer = _repoSupervisor.Customer.AddNewCustomer(CustomerEntityModel);
				if (customer != null)
				{
					_repoSupervisor.Complete();

					CustomerEntityModel.Customer_Number = "C" + customer.Entity.customerid.ToString();
					_repoSupervisor.Customer.UpdateCustomerNumber(customer.Entity.customerid, CustomerEntityModel.Customer_Number);
					_repoSupervisor.Complete();

					return customer.Entity.customerid;
				}
			}
			catch (Exception ex)
			{
				var error = ex.InnerException;
			}

			return 0;
		}

		public bool UpdateCustomer(CustomerEntityModel CustomerEntityModel)
		{
			//Mobile - ISD Code
			CustomerEntityModel.Mobile = new Common().AddMobileISDCode(CustomerEntityModel.Mobile);

			if (_repoSupervisor.Customer.UpdateCustomer(CustomerEntityModel))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public CustomerEntityModel GetCustomerDetailByName(string CustomerName)
		{
			return _repoSupervisor.Customer.GetCustomerDetailByName(CustomerName);
		}

		public IEnumerable<CustomerLoginModel> GetAllCustomerLogin()
		{
			return _repoSupervisor.Customer.GetAllCustomerLogin();
		}

		public long IsCustomerExist(string CustomerName)
		{
			var customer = _repoSupervisor.Customer.GetCustomerDetailByName(CustomerName);
			if (customer != null && customer.Customerid.HasValue && customer.Customerid.Value > 0)
			{
				return customer.Customerid.Value;
			}
			return 0;
		}

		public bool DeleteCustomer(long id)
		{
			if (_repoSupervisor.Customer.DeleteCustomer(id))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public bool ActiveInactiveCustomer(long id, bool status)
		{
			if (_repoSupervisor.Customer.ActiveInactiveCustomer(id, status))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public List<CustomerEntityModel> GetCustomersByKey(string key)
		{
			return _repoSupervisor.Customer.GetCustomersByKey(key);
		}

		public bool ActiveInactiveCustomerLogin(long id, bool status)
		{
			if (_repoSupervisor.Customer.ActiveInactiveCustomerLogin(id, status))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public bool ApprovedCustomerLogin(long id, bool status)
		{
			if (_repoSupervisor.Customer.ApprovedCustomerLogin(id, status))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public bool UpdateCustomerToLogin(long id, long customerId, string name, string userName, string Password)
		{
			if (_repoSupervisor.Customer.UpdateCustomerToLogin(id, customerId, name, userName, Password))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}
	}
}
