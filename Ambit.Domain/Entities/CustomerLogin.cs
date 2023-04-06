using System;
using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class CustomerLogin : BaseEntity
	{
		[Key]
		public Int64 customerloginid { get; set; }
		public Int64 customerid { get; set; }
		public string name { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string deviceid { get; set; }
		public bool isapproved { get; set; }
	}
}
