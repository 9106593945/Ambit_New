using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	public class OrderController : BaseAPIController
	{
		private readonly ILogger<OrderController> _logger;
		private readonly IOrderService _OrderService;

		public OrderController(
			ILogger<OrderController> logger,
			IOrderService OrderService
			)
		{
			_logger = logger;
			_OrderService = OrderService;
		}

		[HttpGet]
        [Route("GetByCustId")]
        public IActionResult GetByCustId()
		{
			return _OrderService.GetOrderDetailsByCustomerLoginId();
		}

		[HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody] OrderEntityModel orderEntityModel)
		{
			return _OrderService.AddOrder(orderEntityModel);
		}

		[HttpGet]
        [Route("GetByOrderId")]
        public IActionResult GetByOrderId(int orderId)
		{
			return _OrderService.GetByOrderId(orderId);
		}

		//[HttpPatch]
  //      [Route("AddOrder")]
  //      public IActionResult ChangeStatus([FromBody] CartItemEntityModel cartItemEntityModel)
		//{
		//	return _OrderService.UpsertCart(cartItemEntityModel);
		//}
	}
}
