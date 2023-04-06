using System;
using System.Collections.Generic;

namespace Ambit.AppCore.EntityModels
{
	public class CartEntityModel : BaseEntityModel
	{
		public long CartId { get; set; }
		public string Cart_Number { get; set; }
		public string Description { get; set; }
		public string Customer_Name { get; set; }
		public int CustomerId { get; set; }
		public decimal Total_Amount { get; set; }
		public DateTime? Cart_Date { get; set; }
		public decimal Discount { get; set; }
		public decimal Extra { get; set; }
		public decimal ShippingCharge { get; set; }
		public decimal SubTotal { get; set; }
		public decimal TaxPercent { get; set; }
		public decimal TaxAmount { get; set; }
		public decimal customerDueAmount { get; set; }
		public string Address { get; set; }
		public string Customer_Mobile { get; set; }
		public string ImageRootUrl { get; set; }
		public string ImageRootPath { get; set; }
		public virtual IEnumerable<CartItemEntityModel> CartItems { get; set; }
	}
}
