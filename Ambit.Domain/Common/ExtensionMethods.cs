using System.Security.Cryptography;

namespace Ambit.Domain.Common
{
	public static class ExtensionMethods
	{
		public static bool IsImage(this string filePath)
		{
			var imageExtensions = new string[]{
			 ".jpg",
			 ".jpeg",
			 ".png",
			 ".gif",
			 ".svg"
		  };
			var extension = Path.GetExtension(filePath).ToLower();

			return imageExtensions.Contains(extension);
		}

		public static string ToUpperString(this Guid guid)
		{
			return guid.ToString().ToUpperInvariant();
		}

		public static string TrimToMaxLength(this string str, int length)
		{
			if (str != null)
			{
				return str.Length > length ? str.Substring(0, length) : str;
			}
			return string.Empty;
		}

		public static Guid? ToGuid(this string guidString)
		{
			Guid guid;
			if (Guid.TryParse(guidString, out guid))
			{
				return guid;
			}
			return null;
		}

		public static double ToDouble(this string value)
		{
			double dblValue;
			if (double.TryParse(value, out dblValue))
			{
				return dblValue;
			}
			return 0;
		}

		public static string GenerateRandomOtp(int digit, bool numericOnly = false)
		{
			string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
			if (numericOnly)
			{
				chars = "1234567890";
			}
			return new string(Enumerable.Repeat(chars, digit).Select(s => s[RandomNumberGenerator.GetInt32(0, s.Length)]).ToArray());
		}
	}
}
