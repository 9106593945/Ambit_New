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
		public IActionResult Get()
		{
			return _OrderService.GetCustomerCartDetailsById();
		}

		[HttpPost]
		public IActionResult Submit([FromBody] CartItemEntityModel cartItemEntityModel)
		{
			return _OrderService.UpsertCart(cartItemEntityModel);
		}

		[HttpPatch]
		public IActionResult ChangeStatus([FromBody] CartItemEntityModel cartItemEntityModel)
		{
			return _OrderService.UpsertCart(cartItemEntityModel);
		}
	}
}
