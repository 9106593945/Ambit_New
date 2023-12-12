using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class OrderItemEntityModel : BaseEntityModel
	{
        [Key]
        public long Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public string? customername { get; set; }
    }
}
