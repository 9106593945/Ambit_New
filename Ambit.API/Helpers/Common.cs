using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ambit.API.Helpers
{
	public class Common
	{
		public static string CleanInput(string strIn)
		{
			// Replace invalid characters with empty strings.
			try
			{
				return Regex.Replace(strIn, @"[^\w\.@-]", "",
								 RegexOptions.None, TimeSpan.FromSeconds(1.5));
			}
			// If we timeout when replacing invalid characters,
			// we should return Empty.
			catch (RegexMatchTimeoutException)
			{
				return String.Empty;
			}
		}

		public static void OptimizeImage(Bitmap originalImg, string filePath, string extenstion)
		{
			Bitmap bm = null;
			Graphics gp = null;

			int width = originalImg.Width;
			int height = originalImg.Height;


			bm = new Bitmap(width, height);
			gp = Graphics.FromImage(bm);
			gp.InterpolationMode = InterpolationMode.HighQualityBicubic;
			gp.CompositingQuality = CompositingQuality.HighQuality;
			gp.SmoothingMode = SmoothingMode.HighQuality;

			gp.DrawImage(originalImg, 0, 0, width, height);
			switch (extenstion)
			{
				case ".jpg":
				case ".jpeg":
					bm.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
					break;
				case ".png":
					bm.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
					break;
				case ".gif":
					bm.Save(filePath, System.Drawing.Imaging.ImageFormat.Gif);
					break;
				default:
					bm.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
					break;
			}
		}

		public static string HtmlToPdf(string pdfOutputLocation, string outputFilenamePrefix, string[] urls,
		  string[] options = null,
		  string pdfHtmlToPdfExePath = "C:\\Program Files\\wkhtmltopdf\\bin\\wkhtmltopdf.exe")
		{
			string urlsSeparatedBySpaces = string.Empty;
			try
			{
				//Determine inputs
				if ((urls == null) || (urls.Length == 0))
					throw new Exception("No input URLs provided for HtmlToPdf");
				else
					urlsSeparatedBySpaces = String.Join(" ", urls); //Concatenate URLs

				string outputFolder = pdfOutputLocation;
				string outputFilename = outputFilenamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".PDF"; // assemble destination PDF file name

				var p = new System.Diagnostics.Process()
				{
					StartInfo =
					{
					    FileName = pdfHtmlToPdfExePath,
					    Arguments = ((options == null) ? "" : String.Join(" ", options)) + " " + urlsSeparatedBySpaces + " " + outputFilename,
					    UseShellExecute = false, // needs to be false in order to redirect output
					    RedirectStandardOutput = true,
					    RedirectStandardError = true,
					    RedirectStandardInput = true, // redirect all 3, as it should be all 3 or none
					    WorkingDirectory = pdfOutputLocation
					}
				};

				p.Start();

				// read the output here...
				var output = p.StandardOutput.ReadToEnd();
				var errorOutput = p.StandardError.ReadToEnd();

				// ...then wait n milliseconds for exit (as after exit, it can't read the output)
				p.WaitForExit(60000);

				// read the exit code, close process
				int returnCode = p.ExitCode;
				p.Close();

				// if 0 or 2, it worked so return path of pdf
				if ((returnCode == 0) || (returnCode == 2))
					return outputFolder + outputFilename;
				else
					throw new Exception(errorOutput);
			}
			catch (Exception exc)
			{
				throw new Exception("Problem generating PDF from HTML, URLs: " + urlsSeparatedBySpaces + ", outputFilename: " + outputFilenamePrefix, exc);
			}
		}

		public string AddMobileISDCode(string mobileNo)
		{
			if (((mobileNo ?? "") != "") && (!mobileNo.StartsWith("+91")))
			{
				mobileNo = "+91" + mobileNo;
			}
			return mobileNo;
		}
	}
}
