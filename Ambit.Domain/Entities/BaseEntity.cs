﻿using System;

namespace Ambit.Domain.Entities
{
	public class BaseEntity
	{
		public bool? Active { get; set; }
		public bool? isDeleted { get; set; }
		public DateTime? Created_On { get; set; }
		public long? Created_By { get; set; }
		public DateTime? Updated_On { get; set; }
		public long? Updated_By { get; set; }
	}
}
