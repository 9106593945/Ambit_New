using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class cart : BaseEntity
	{
		public int cartid { get; set; }
		public string customername { get; set; }
		public bool Active { get; set; }
		public bool isDeleted { get; set; }
		public DateTime? Created_On { get; set; }
		public int Created_By { get; set; }
		public DateTime? Updated_On { get; set; }
		public int Updated_By { get; set; }
		public int customerloginid { get; set; }

	}
}
