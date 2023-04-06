using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
    }
}
