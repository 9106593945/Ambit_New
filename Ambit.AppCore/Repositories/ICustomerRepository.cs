using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ambit.AppCore.Repositories
{
	public interface ICustomerRepository
	{
		IEnumerable<CustomerEntityModel> GetAllCustomers();
		CustomerEntityModel GetCustomerById(int Id);
		bool UpdateCustomer(CustomerEntityModel CustomerEntityModel);
		EntityEntry<Customer> AddNewCustomer(CustomerEntityModel CustomerEntityModel);
		bool IsCustomerCodeExist(string CustomerCode);
		bool DeleteCustomer(long id);
		IEnumerable<CustomerEntityModel> AllCustomers();
		bool ActiveInactiveCustomer(long id, bool status);
		List<CustomerEntityModel> GetCustomersByKey(string key);
		bool UpdateCustomerNumber(long customerId, string customerNumber);
		CustomerEntityModel GetCustomerDetailByName(string CustomerName);
		IEnumerable<CustomerLoginModel> GetAllCustomerLogin();
		bool ActiveInactiveCustomerLogin(long id, bool status);
		bool ApprovedCustomerLogin(long id, bool status);
		bool UpdateCustomerToLogin(long id, long customerId, string name, string userName, string Password);
	}
}
