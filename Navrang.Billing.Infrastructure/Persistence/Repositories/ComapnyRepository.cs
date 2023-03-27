using Navrang.Billing.AppCore.EntityModels;
using Navrang.Billing.AppCore.Repositories;
using System.Linq;

namespace Navrang.Billing.Infrastructure.Persistence.Repositories
{
	public class ComapnyRepository : BaseRepository, ICompanyRepository
	{
		public ComapnyRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public CompanyEntityModel GetCompany()
		{
			var CompanyDetails = _dbContext.Company.Where(a => a.isDeleted == false)
				.Select(x => new CompanyEntityModel()
				{
					accountnumber = x.account_number,
					address = x.address,
					billingaddress = x.billing_address,
					contactperson = x.contact_person,
					bankifsc = x.bank_ifsc,
					bankname = x.bank_name,
					city = x.city,
					state = x.state,
					country = x.country,
					name = x.name,
					companyid = x.companyid,
					emailaddress = x.email_address,
					gstin = x.gst_number,
					logopath = x.logo_path,
					mobile = x.mobile,
					phone = x.phone,
					pincode = x.postcode,
					terms = x.terms,
					website = x.website
				}).FirstOrDefault();
			if (CompanyDetails == null)
				return null;

			return CompanyDetails;
		}

		public bool UpdateCompanyDetails(CompanyEntityModel companyEntityModel)
		{
			var CompanyDetails = _dbContext.Company.Find(companyEntityModel.companyid);

			if (CompanyDetails == null)
				return false;

			CompanyDetails.name = companyEntityModel.name;
			CompanyDetails.account_number = companyEntityModel.accountnumber;
			CompanyDetails.address = companyEntityModel.address;
			CompanyDetails.bank_ifsc = companyEntityModel.bankifsc;
			CompanyDetails.bank_name = companyEntityModel.bankname;
			CompanyDetails.billing_address = companyEntityModel.billingaddress;
			CompanyDetails.city = companyEntityModel.city;
			CompanyDetails.contact_person = companyEntityModel.contactperson;
			CompanyDetails.country = companyEntityModel.country;
			CompanyDetails.email_address = companyEntityModel.emailaddress;
			CompanyDetails.gst_number = companyEntityModel.gstin;
			CompanyDetails.logo_path = companyEntityModel.logopath;
			CompanyDetails.mobile = companyEntityModel.mobile;
			CompanyDetails.phone = companyEntityModel.phone;
			CompanyDetails.state = companyEntityModel.state;
			CompanyDetails.terms = companyEntityModel.terms;
			CompanyDetails.postcode = companyEntityModel.pincode;
			CompanyDetails.website = companyEntityModel.website;

			return true;
		}
	}
}
