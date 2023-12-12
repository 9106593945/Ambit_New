using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ambit.AppCore.Repositories
{
	public interface IOrderRepository
	{
		bool UpdateCart(CartEntityModel CartEntityModel);
		EntityEntry<Order> AddOrder(OrderEntityModel OrderEntityModel);
		bool DeleteCart(long id);
		bool ActiveInactiveCart(long id, bool status);
		List<ItemEntityModel> GetSerchItems(string term);
		EntityEntry<OrderItems> AddOrderItems(OrderItemEntityModel OrderItemEntityModel);
		bool UpdateCartItems(CartItemEntityModel CartItemEntity);
		List<CartItemEntityModel> GetCartItemsByCartId(int Id);
		bool DeleteCartItems(long id);
		List<OrderEntityModel> GetOrderDetailsByCustomerLoginId(int customerloginid);
        List<OrderItemEntityModel> GetOrderItemsById(int orderitemId);
		OrderEntityModel GetByOrderId(int orderId);
		decimal getCustomerTotalAmountById(long customerId);
		int IsCartExist(int customerloginid);
	}
}
