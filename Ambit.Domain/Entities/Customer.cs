using System;
using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class Customer : BaseEntity
	{
		[Key]
		public Int64 customerid { get; set; }
		public string Name { get; set; }
		public string customer_number { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string postcode { get; set; }
		public string state { get; set; }
		public string country { get; set; }
		public string mobile { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string description { get; set; }
		public string transport { get; set; }
	}
}
