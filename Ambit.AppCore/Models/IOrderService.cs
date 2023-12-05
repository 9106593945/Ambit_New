using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.AppCore.Models
{
	public interface IOrderService
	{
		bool DeleteCart(long id);
		ObjectResult AddCartItems(CartItemEntityModel CartItemEntity);
		ObjectResult GetCustomerCartDetailsById();
		ObjectResult UpsertCart(CartItemEntityModel cartItemEntityModel);
	}
}
