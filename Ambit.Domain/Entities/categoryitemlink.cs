using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class categoryitemlink : BaseEntity
	{
		[Key]
		public long categoryitemlinkid { get; set; }
		public long categoryid { get; set; }
		public long itemid { get; set; }
	}
}
