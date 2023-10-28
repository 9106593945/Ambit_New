using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class RegisterRequestModel
	{
		[Required(ErrorMessage = "Please enter Name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please enter Password")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please enter Device Id")]
		public string DeviceId { get; set; }
		[Required(ErrorMessage = "Please enter User Name")]
		public string username { get; set; }
		public string InvitationCode { get; set; }
		public string Type { get; set; }
		//public long customerId { get; set; }
	}
}
