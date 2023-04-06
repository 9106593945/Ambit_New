using System;
using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class favoriteitem : BaseEntity
	{
		[Key]
		public long favoriteitemid { get; set; }
		public long itemid { get; set; }
		public long customerid { get; set; }
		public bool isfavorite { get; set; }
		public DateTime? favoriteitemdate { get; set; }
	}
}
