using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	public class CartController : BaseAPIController
	{
		private readonly ILogger<CartController> _logger;
		private readonly ICartService _CartService;

		public CartController(
			ILogger<CartController> logger,
			ICartService CartService
			)
		{
			_logger = logger;
			_CartService = CartService;
		}

		[HttpGet]
		public IActionResult Get(int id)
		{
			return _CartService.getCustomerCartDetailsById(id);
		}

		[HttpPost]
		public IActionResult UpsertCart([FromBody] CartItemEntityModel cartItemEntityModel)
		{
			return _CartService.UpsertCart(cartItemEntityModel);
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var delte = _CartService.DeleteCart(id);
			return new OkObjectResult(delte);
		}
	}
}
