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
		public IActionResult Get([FromForm] int customerId)
		{
			var cartItems = _CartService.getCustomerCartDetailsById(customerId);
			var response = new CommonAPIReponse<List<CartItemEntityModel>>() { Success = true, Message = "", data = cartItems };

			return new OkObjectResult(response);
		}

		[HttpPost]
		public IActionResult UpsertCart([FromBody] CartItemEntityModel cartItemEntityModel)
		{
			var cartItems = _CartService.UpsertCart(cartItemEntityModel);

			return new OkObjectResult(cartItems);
		}

		[HttpDelete]
		public IActionResult Delete([FromForm] int id)
		{
			var delte = _CartService.DeleteCart(id);
			return new OkObjectResult(delte);
		}
	}
}
