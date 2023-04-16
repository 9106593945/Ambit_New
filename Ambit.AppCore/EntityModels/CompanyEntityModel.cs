namespace Ambit.AppCore.EntityModels
{
	public class CompanyEntityModel : BaseEntityModel
	{
		public Guid companyid { get; set; }
		public string name { get; set; }
		public string contactperson { get; set; }
		public string address { get; set; }
		public string billingaddress { get; set; }
		public string city { get; set; }
		public string gstin { get; set; }
		public string state { get; set; }
		public string pincode { get; set; }
		public string country { get; set; }
		public string phone { get; set; }
		public string emailaddress { get; set; }
		public string mobile { get; set; }
		public string website { get; set; }
		public string bankname { get; set; }
		public string terms { get; set; }
		public string logopath { get; set; }
		public string accountnumber { get; set; }
		public string bankifsc { get; set; }
	}
}
