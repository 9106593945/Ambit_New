using Ambit.API.Helpers;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ambit.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class CategoryController : ControllerBase
	{
		private readonly ILogger<CategoryController> _logger;
		private readonly IitemService _itemService;

		public CategoryController(
			ILogger<CategoryController> logger,
		  IitemService itemService
			)
		{
			_logger = logger;
			_itemService = itemService;
		}


		[Route("GetAllCategory")]
		[HttpPost]
		public IActionResult GetAllCategory()
		{
			IEnumerable<CategoryEntityModel> CategoryItems = _itemService.GetAllCategory();
			var response = new List<CategoryEntityModel>();
			response = CategoryItems.Select(s => new CategoryEntityModel()
			{
				Name = s.Name,
				Description = s.Description,
				ImagePath = s.ImagePath,
				Image = s.Image,
				CategoryId = s.CategoryId
			}).ToList();

			return Ok(response);
		}

		//[Route("ClearAllFavorite")]
		//[HttpPost]
		//public IActionResult ClearAllFavorite([FromForm] int customerId, [FromForm] int customerLoginId)
		//{
		//    bool favoriteItems = _itemService.ClearAllFavorite(customerLoginId);
		//    return Ok(new JObject { { "message", "Favorite Items clear successfully." } });
		//}
		//[HttpPost]
		//public IActionResult Test([FromForm] FavoriteItemRequestModel favoriteItemRequestModel)
		//{
		//    return Ok(new JObject { { "message", "Favorite Items clear successfully." } });
		//}
		//[Route("UpsertFavoriteItem")]
		//[HttpPost]
		//public IActionResult UpsertFavoriteItem([FromForm] FavoriteItemRequestModel favoriteItemRequestModel)
		//{
		//    bool favoriteItem = _itemService.UpsertFavoriteItem(favoriteItemRequestModel.customerLoginId, favoriteItemRequestModel.itemId, favoriteItemRequestModel.isFavorite);
		//    if (favoriteItem)
		//    {
		//        IEnumerable<ItemAPIEntityModel> favoriteItems = _itemService.GetAllFavoriteItem(favoriteItemRequestModel.customerLoginId)
		//            .Select(s => new ItemAPIEntityModel()
		//            {
		//                Code = s.Code,
		//                Image = s.Image,
		//                ImagePath = s.ImagePath,
		//                Description = s.Description,
		//                FavoriteItemId = s.favoriteitemId,
		//                ItemId = s.ItemId,
		//                Name = s.Name,
		//                IsFavorite = s.IsFavorite,
		//                SellAmount = s.SellAmount
		//            });

		//        return Ok(favoriteItems);
		//    }

		//    return BadRequest();
		//}

	}
}
