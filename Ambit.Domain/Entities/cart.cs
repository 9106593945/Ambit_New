using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class cart : BaseEntity
	{
		[Key]
		public long cartid { get; set; }
		public string cartnumber { get; set; }
		public string description { get; set; }
		public string customername { get; set; }
		public int customerid { get; set; }
		public decimal total_amount { get; set; }
		public DateTime? cartdate { get; set; }
		public decimal discount { get; set; }
		public decimal extra { get; set; }
		public decimal? shippingcharge { get; set; }
		public decimal? taxpercent { get; set; }
		public decimal? taxamount { get; set; }
		public decimal amount { get; set; }
		public string address { get; set; }
	}
}
