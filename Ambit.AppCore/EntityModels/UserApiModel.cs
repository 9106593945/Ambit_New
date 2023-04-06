using System;
using System.Collections.Generic;
using System.Text;

namespace Ambit.AppCore.EntityModels
{
	public class UserApiModel
	{
		public long Id { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public int? Type { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
		public string Token { get; set; }
		public bool isApproved { get; set; }
		public string deviceId { get; set; }
		public long? customerId { get; set; }

	}
}
