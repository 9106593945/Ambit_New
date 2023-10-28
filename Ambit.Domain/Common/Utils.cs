using Microsoft.AspNetCore.Mvc;

namespace Ambit.Domain.Common
{
	public static class Utils
	{
		public static ObjectResult GetObjectResult(int status, object value)
		{
			return new ObjectResult("")
			{
				StatusCode = status,
				Value = value
			};
		}
	}
}
