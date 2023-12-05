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


		[HttpGet]
		public IActionResult Get([FromQuery] CategoryItemRequest request)
		{
			IEnumerable<ItemEntityModel> CategoryItems = _itemService.GetAllItems(request);

			var response = new CommonAPIReponse<dynamic>()
			{
				Data = CategoryItems.Select(s => new
				{
					Code = s.Code,
					Image = s.Image,
					ImagePath = _appSettings.SiteUrl + "/images/items/resize/" + (!string.IsNullOrEmpty(s.Image) ? s.Image : "no-image.png"),
					Description = s.Description,
					FavoriteItemId = s.FavoriteItemId,
					ItemId = s.ItemId,
					Name = s.Name,
					IsFavorite = s.IsFavorite,
					SellAmount = s.SellAmount,
					DefaultAddQty = 1
				}).ToList(),
				Message = "Items retrived successfully.",
				Status = 200
			};
			return Ok(response);
		}

		[HttpGet("ItemInfoById")]
		public IActionResult ItemsInfo(int id, int customerid)
		{
			ItemEntityModel itemDetail = _itemService.GetItemByID(id, customerid);
			itemDetail.ProductImages.Add(new ProductImage
			{
				Id = 1,
				ImagePath = _appSettings.SiteUrl + "/images/items/resize/" + (!string.IsNullOrEmpty(itemDetail.Image) ? itemDetail.Image : "no-image.png")
			});
			return Ok(itemDetail);
		}
	}
}
