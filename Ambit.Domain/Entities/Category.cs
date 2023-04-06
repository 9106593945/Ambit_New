using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class category : BaseEntity
	{
		[Key]
		public long categoryid { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string image { get; set; }
	}
}
