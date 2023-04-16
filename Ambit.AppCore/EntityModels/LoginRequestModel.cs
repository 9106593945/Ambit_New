using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class LoginRequestModel
	{
		[Required(ErrorMessage = "Please enter User Name")]
		public string userName { get; set; }
		[Required(ErrorMessage = "Please enter Password")]
		public string password { get; set; }
		public string deviceid { get; set; }
		public bool remember { get; set; }
	}
}
