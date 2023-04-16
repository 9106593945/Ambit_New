using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Models
{
	public interface ICustomerService
	{
		IEnumerable<CustomerEntityModel> GetCustomerList();
		CustomerEntityModel GetCustomerByID(int Id);
		long AddNewCustomer(CustomerEntityModel CustomerEntityModel);
		bool UpdateCustomer(CustomerEntityModel CustomerEntityModel);
		bool DeleteCustomer(long id);
		bool ActiveInactiveCustomer(long id, bool status);
		long IsCustomerExist(string CustomerCode);
		List<CustomerEntityModel> GetCustomersByKey(string key);
		CustomerEntityModel GetCustomerDetailByName(string CustomerName);
		IEnumerable<CustomerLoginModel> GetAllCustomerLogin();
		bool ActiveInactiveCustomerLogin(long id, bool status);
		bool ApprovedCustomerLogin(long id, bool status);
		bool UpdateCustomerToLogin(long id, long customerId, string name, string userName, string Password);
	}
}
