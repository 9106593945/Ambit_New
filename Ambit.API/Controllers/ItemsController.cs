using Ambit.API.Helpers;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ambit.API.Controllers
{
	public class ItemsController : BaseAPIController
	{
		private readonly ILogger<ItemsController> _logger;
		private readonly IitemService _itemService;
		private readonly AppSettings _appSettings;

		public ItemsController(
			ILogger<ItemsController> logger,
		  IitemService itemService,
			IOptions<AppSettings> appSettings
			)
		{
			_logger = logger;
			_itemService = itemService;
			_appSettings = appSettings.Value;
		}


		[Route("GetAllItems")]
		[HttpPost]
		public IActionResult GetAllItems([FromForm] int categoryid, [FromForm] int customerid, [FromForm] int customerLoginId)
		{
			IEnumerable<ItemEntityModel> CategoryItems = _itemService.GetAllItems(categoryid, customerid, customerLoginId);

			var response = new CommonAPIReponse<dynamic>()
			{
				data = CategoryItems.Select(s => new
				{
					Code = s.Code,
					Image = s.Image,
					ImagePath = s.ImagePath,
					Description = s.Description,
					FavoriteItemId = s.favoriteitemId,
					ItemId = s.ItemId,
					Name = s.Name,
					IsFavorite = s.IsFavorite,
					SellAmount = s.SellAmount
				}).ToList(),
				Message = "Items retrived successfully.",
				Success = true
			};
			return Ok(response);
		}

		[HttpGet("ItemInfoById")]
		public IActionResult ItemsInfo(int id, int customerid)
		{
			ItemEntityModel itemDetail = _itemService.GetItemByID(id, customerid);
			itemDetail.ImagePath = "/images/items/resize/" + (!string.IsNullOrEmpty(itemDetail.Image) ? itemDetail.Image : "no-image.png");
			return Ok(itemDetail);
		}
	}
}
