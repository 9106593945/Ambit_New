using System;
using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class CustomerLoginModel : BaseEntityModel
	{
		public Int64? Customerid { get; set; }
		public Int64? CustomerLoginid { get; set; }
		public string Name { get; set; }
		public string Customer_Number { get; set; }
		public string Customer_Name { get; set; }
		public string Mobile { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string deviceId { get; set; }
		public bool isApproved { get; set; }

	}
}
