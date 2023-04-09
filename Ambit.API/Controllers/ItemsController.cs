using Ambit.API.Helpers;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Ambit.API.Controllers
{
    [Route("[controller]")]
	[ApiController]
	[Authorize]
	public class ItemsController : ControllerBase
	{
		private readonly ILogger<FavoriteController> _logger;
        private readonly IitemService _itemService;
        private readonly AppSettings _appSettings;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IConfiguration _configuration;

		public ItemsController(
			ILogger<FavoriteController> logger,
            IitemService itemService,
			IOptions<AppSettings> appSettings,
			IConfiguration configuration
			)
		{
			_logger = logger;
            _itemService = itemService;
            _appSettings = appSettings.Value;
			_configuration = configuration;
		}


        [Route("GetAllItems")]
        [HttpPost]
        public IActionResult GetAllItems([FromForm] int categoryid, [FromForm] int customerid, [FromForm] int customerLoginId)
        {
            IEnumerable<ItemEntityModel> CategoryItems = _itemService.GetAllItems(categoryid, customerid, customerLoginId);
            var response = new List<ItemAPIEntityModel>();
            response = CategoryItems.Select(s => new ItemAPIEntityModel()
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
