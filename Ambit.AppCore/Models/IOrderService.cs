using Ambit.AppCore.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.AppCore.Models
{
	public interface IOrderService
	{
		bool DeleteCart(long id);
		ObjectResult AddOrder(OrderEntityModel orderEntityModel);
		ObjectResult GetOrderDetailsByCustomerLoginId();
		ObjectResult GetByOrderId(int orderId);

        ObjectResult UpsertCart(CartItemEntityModel cartItemEntityModel);
	}
}
