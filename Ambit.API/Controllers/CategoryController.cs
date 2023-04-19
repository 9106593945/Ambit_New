using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	public class CategoryController : BaseAPIController
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

		[HttpGet]
		public IActionResult GetAllCategory()
		{
			IEnumerable<CategoryEntityModel> CategoryItems = _itemService.GetAllCategory();

			var response = new CommonAPIReponse<dynamic>()
			{
				data = CategoryItems.Select(s => new
				{
					s.Name,
					s.Description,
					s.ImagePath,
					s.Image,
					s.CategoryId
				}).ToList(),
				Message = "Category retrived successfully.",
				Success = true
			};

			return Ok(response);
		}
	}
}
