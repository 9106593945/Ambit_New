using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class LoginRequestModel
	{
		[Required(ErrorMessage = "Please enter User Name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Please enter Password")]
		public string Password { get; set; }
		public string DeviceId { get; set; }
	}
}
