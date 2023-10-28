using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class CustomerLogin : BaseEntity
	{
		[Key]
		public long Customerloginid { get; set; }
		public long Customerid { get; set; }
		public string Name { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Deviceid { get; set; }
		public bool Isapproved { get; set; }
		public string Invitationcode { get; set; }
		public string Type { get; set; }
		public int ParentId { get; set; }
	}
}
