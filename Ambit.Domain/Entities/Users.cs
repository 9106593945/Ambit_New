using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class Users : BaseEntity
	{
		[Key]
		public Int64 UserId { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public DateTime? Last_Login { get; set; }
		public string Role { get; set; }

	}
}
