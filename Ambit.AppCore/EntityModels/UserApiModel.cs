using Newtonsoft.Json;

namespace Ambit.AppCore.EntityModels
{
	public class UserApiModel
	{
		public long Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public int? Type { get; set; }
		[JsonIgnore]
		public string Password { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public bool isApproved { get; set; }
		public string deviceId { get; set; } = string.Empty;
		public long? customerId { get; set; }
		public string invitationcode { get; set; } = string.Empty;

	}
}
