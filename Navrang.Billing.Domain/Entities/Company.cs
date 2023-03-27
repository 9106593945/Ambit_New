using System;
using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.Domain.Entities
{

	public class Company : BaseEntity
	{
		[Key]
		public Guid companyid { get; set; }
		public string name { get; set; }
		public string contact_person { get; set; }
		public string address { get; set; }
		public string billing_address { get; set; }
		public string city { get; set; }
		public string gst_number { get; set; }
		public string state { get; set; }
		public string postcode { get; set; }
		public string country { get; set; }
		public string phone { get; set; }
		public string email_address { get; set; }
		public string mobile { get; set; }
		public string website { get; set; }
		public string bank_name { get; set; }
		public string terms { get; set; }
		public string logo_path { get; set; }
		public string account_number { get; set; }
		public string bank_ifsc { get; set; }
	}
}
