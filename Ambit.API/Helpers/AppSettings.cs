namespace Ambit.API.Helpers
{
	public class AppSettings
	{
		public string SecretKey { get; set; }
		public string SiteUrl { get; set; }
		public int? TokenExpireTime { get; set; }
	}
}
