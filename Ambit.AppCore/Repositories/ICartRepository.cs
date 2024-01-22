using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ambit.AppCore.Repositories
{
	public interface ICartRepository
	{
		bool UpdateCart(CartEntityModel CartEntityModel);
		EntityEntry<cart> AddNewCart(CartEntityModel CartEntityModel);
		bool DeleteCart(long id);
		bool ActiveInactiveCart(long id, bool status);
		List<ItemEntityModel> GetSerchItems(string term);
		EntityEntry<cartitems> AddCartItems(CartItemEntityModel CartItemEntityModel);
		bool UpdateCartItems(CartItemEntityModel CartItemEntity);
		List<CartItemEntityModel> GetCartItemsByCartId(int Id);
		bool DeleteCartItems(long id);
		List<CartItemEntityModel> GetCartDetailsByCustomerLoginId(int customerloginid);
		decimal getCustomerTotalAmountById(long customerId);
		long IsCartExist(int customerloginid);
	}
}
