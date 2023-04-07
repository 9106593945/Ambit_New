using Ambit.API.Helpers;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Ambit.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class FavoriteController : ControllerBase
	{
		private readonly ILogger<FavoriteController> _logger;
        private readonly IitemService _itemService;
        private readonly AppSettings _appSettings;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IConfiguration _configuration;

		public FavoriteController(
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


        [Route("GetAllFavorite")]
        [HttpPost]
        public IActionResult GetAllFavorite([FromForm] int customerId, [FromForm] int customerLoginId)
        {
            IEnumerable<ItemEntityModel> favoriteItems = _itemService.GetAllFavoriteItem(customerLoginId);
            var response = new List<ItemAPIEntityModel>();
            response = favoriteItems.Select(s => new ItemAPIEntityModel()
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

        [Route("ClearAllFavorite")]
        [HttpPost]
        public IActionResult ClearAllFavorite([FromForm] int customerId, [FromForm] int customerLoginId)
        {
            bool favoriteItems = _itemService.ClearAllFavorite(customerLoginId);
            return Ok(new JObject { { "message", "Favorite Items clear successfully." } });
        }

        [Route("UpsertFavoriteItem")]
        [HttpPost]
        public IActionResult UpsertFavoriteItem([FromForm] int itemId, [FromForm] int customerId, [FromForm] int customerLoginId, [FromForm] bool isFavorite)
        {
            bool favoriteItem = _itemService.UpsertFavoriteItem(customerLoginId, itemId, isFavorite);
            if (favoriteItem)
            {
                IEnumerable<ItemAPIEntityModel> favoriteItems = _itemService.GetAllFavoriteItem(customerLoginId)
                    .Select(s => new ItemAPIEntityModel()
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
                    });

                return Ok(favoriteItems);
            }

            return BadRequest();
        }

    }
}
