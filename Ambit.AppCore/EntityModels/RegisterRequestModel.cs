using System.ComponentModel.DataAnnotations;

namespace Ambit.AppCore.EntityModels
{
	public class RegisterRequestModel
	{
		[Required(ErrorMessage = "Please enter Name")]
		public string name { get; set; }
		[Required(ErrorMessage = "Please enter Password")]
		public string password { get; set; }
		[Required(ErrorMessage = "Please enter Device Id")]
		public string deviceId { get; set; }
		[Required(ErrorMessage = "Please enter User Name")]
		public string username { get; set; }
		//public long customerId { get; set; }
	}
}
