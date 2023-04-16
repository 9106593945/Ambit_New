using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class ItemEntityModel : BaseEntityModel
	{
		public long? favoriteitemId { get; set; }
		public long? ItemId { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string ImagePath { get; set; }
		public IFormFile file { get; set; }
		public string Code { get; set; }
		public string Supplier { get; set; }
		public int OpeningQuantity { get; set; }
		public int Stock { get; set; }
		public decimal PurchaseAmount { get; set; }

		[Display(Name = "Selling Price")]
		public decimal SellAmount { get; set; }

		[Display(Name = "Retail Price")]
		public decimal RetailAmount { get; set; }
		public decimal MRP { get; set; }
		public decimal? Discount { get; set; }
		public string CategoryIds { get; set; }
		public bool IsFavorite { get; set; }
		public bool ShownInApp { get; set; }
		public bool IsComboProduct { get; set; }

		public List<CategoryEntityModel> Category { get; set; }

		public int TotalCount { get; set; }
		public List<ComboProductSubItemEntity> SubItems { get; set; }

	}

	public class ComboProductSubItemEntity : BaseEntityModel
	{
		public long ComboProductSubItemId { get; set; }
		public long ItemId { get; set; }
		public long ParentItemId { get; set; }
		public string Code { get; set; }
		public string Action { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public string ImagePath { get; set; }
		public int Quantity { get; set; }
		public decimal SubTotal { get; set; }
		public decimal SellAmount { get; set; }
		public decimal PurchaseAmount { get; set; }
		public decimal RetailAmount { get; set; }
		public decimal Discount { get; set; }
		public decimal Mrp { get; set; }
	}
}
