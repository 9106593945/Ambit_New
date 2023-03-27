using Navrang.Billing.AppCore.EntityModels;

namespace Navrang.Billing.AppCore.Models
{
	public interface ICompanyService
	{
		CompanyEntityModel GetCompanyDetails();
		bool UpdateCompanyDetails(CompanyEntityModel companyEntityModel);
	}
}
