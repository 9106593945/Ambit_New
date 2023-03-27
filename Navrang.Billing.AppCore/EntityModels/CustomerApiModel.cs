using System;
using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.AppCore.EntityModels
{
	public class CustomerEntityModel : BaseEntityModel
	{
		public Int64? Customerid { get; set; }
		[Required(ErrorMessage ="Name is required")]
		public string Name { get; set; }
		public string Customer_Number { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostCode { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public decimal dueAmount { get; set; }
		//public string Password { get; set; }
		public string Description { get; set; }
		public string Transport { get; set; }

	}
}
