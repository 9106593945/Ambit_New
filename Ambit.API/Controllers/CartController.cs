using Ambit.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Ambit.AppCore.Models;

namespace Ambit.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CartController : ControllerBase
	{
		private readonly ILogger<CartController> _logger;
		private readonly ICartService _CartService;
		private readonly AppSettings _appSettings;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IConfiguration _configuration;
		//private readonly IGeneratePdf _generatePdf;

		public CartController(
			ILogger<CartController> logger,
			ICartService CartService,
			IOptions<AppSettings> appSettings,
			IConfiguration configuration,
			IWebHostEnvironment hostingEnvironment
			//IGeneratePdf generatePdf
			)
		{
			_logger = logger;
			_CartService = CartService;
			_appSettings = appSettings.Value;
			_configuration = configuration;
			_hostingEnvironment = hostingEnvironment;
		}

		[HttpGet("customer/{id}")]
		public IActionResult Get(int id)
		{
			var cartItems = _CartService.getCustomerCartDetailsById(id);
			return new OkObjectResult(cartItems);
		}

		[HttpPost]
		public void Post([FromBody] string value)
		{

		}

		// PUT api/<CartController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{

		}

		// DELETE api/<CartController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
