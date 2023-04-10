using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using System.Collections.Generic;

namespace Ambit.AppCore.Repositories
{
	public interface ICartRepository
	{
		IEnumerable<CartEntityModel> GetAllCarts();
		CartEntityModel GetCartById(int Id);
		bool UpdateCart(CartEntityModel CartEntityModel);
		EntityEntry<cart> AddNewCart(CartEntityModel CartEntityModel);
		bool IsCartCodeExist(string CartCode);
		bool DeleteCart(long id);
		bool ActiveInactiveCart(long id, bool status);
		bool UpdateCartNumber(long CartId, string CartNumber);
		CartEntityModel GetLastCart();
		List<ItemEntityModel> GetSerchItems(string term);
		EntityEntry<cartitems> AddCartItems(CartItemEntityModel CartItemEntityModel);
		bool UpdateCartItems(CartItemEntityModel CartItemEntity);
		List<CartItemEntityModel> GetCartItemsByCartId(int Id);
		bool DeleteCartItems(long id);
		bool DeleteCartItemsByCartId(long id);
		bool DeleteCartItemsByCartItemId(long id);
		List<CartItemEntityModel> getCustomerCartDetailsById(int customerloginid);
		decimal getCustomerTotalAmountById(long customerId);
        int IsCartExist(int customerloginid);
    }
}
