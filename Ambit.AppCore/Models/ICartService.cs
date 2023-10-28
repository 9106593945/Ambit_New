using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.AppCore.Models
{
	public interface ICartService
	{
		IEnumerable<CartEntityModel> GetCartList();
		CartEntityModel GetCartByID(int Id);
		Int64 AddNewCart(CartEntityModel CartEntityModel, IFormCollection collection);
		bool UpdateCart(CartEntityModel CartEntity, IFormCollection collection);
		bool DeleteCart(long id);
		bool ActiveInactiveCart(long id, bool status);
		bool IsCartCodeExist(string CartCode);
		IEnumerable<CustomerEntityModel> GetAllCustomer();
		CartEntityModel GetLastCart();
		List<ItemEntityModel> GetSerchItems(string term);
		ObjectResult AddCartItems(CartItemEntityModel CartItemEntity);
		ObjectResult getCustomerCartDetailsById(int customerId);
		ObjectResult UpsertCart(CartItemEntityModel cartItemEntityModel);
	}
}
