using System;

namespace Navrang.Billing.Domain.Entities
{
	public class ViewAllItems : BaseEntity
	{
          public Int64 Itemid{ get; set; }
          public string Name { get; set; }
          public string Description { get; set; }
          public string Supplier { get; set; }
          public string Image { get; set; }    
          public string Code { get; set; }
          public string categoryIds { get; set; }
          public int OpeningQuantity { get; set; }
          public int Stock { get; set; }
          public decimal PurchaseAmount { get; set; }
          public decimal SellAmount { get; set; }
          public decimal Discount { get; set; }
          public bool ShownInApp { get; set; }
     }
}
