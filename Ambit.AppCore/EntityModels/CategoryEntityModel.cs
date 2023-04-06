using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class CategoryEntityModel : BaseEntityModel
	{
          public long? CategoryId { get; set; }
          [Required]
          public string Name { get; set; }
          public string Description { get; set; }
          public string Image { get; set; }
          public string ImagePath { get; set; }
          public IFormFile file { get; set; }
     }
}