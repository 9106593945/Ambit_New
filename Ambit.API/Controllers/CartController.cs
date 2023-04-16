using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CartController : ControllerBase
	{
		private readonly ILogger<CartController> _logger;
		private readonly ICartService _CartService;
		//private readonly IGeneratePdf _generatePdf;

		public CartController(
			ILogger<CartController> logger,
			ICartService CartService
			)
		{
			_logger = logger;
			_CartService = CartService;
		}

		[Route("Get")]
		[HttpPost]
		public IActionResult Get([FromForm] int customerId)
		{
			var cartItems = _CartService.getCustomerCartDetailsById(customerId);
			return new OkObjectResult(cartItems);
		}

		[Route("UpsertCart")]
		[HttpPost]
		public IActionResult UpsertCart([FromBody] CartItemEntityModel cartItemEntityModel)
		{
			var cartItems = _CartService.UpsertCart(cartItemEntityModel);
			return new OkObjectResult(cartItems);
		}


		// DELETE api/<CartController>/5
		[Route("Delete")]
		[HttpPost]
		public IActionResult Delete([FromForm] int id)
		{
			var delte = _CartService.DeleteCart(id);
			return new OkObjectResult(delte);

		}
	}
}
