using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.AppCore.Models
{
	public interface ICartService
	{
		bool DeleteCart(long id);
		ObjectResult AddCartItems(CartItemRequest CartItemEntity);
		ObjectResult GetCustomerCartDetailsById();
		ObjectResult UpsertCart(CartItemRequest cartItemEntityModel);
	}
}
