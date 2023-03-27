using Navrang.Billing.AppCore.EntityModels;

namespace Navrang.Billing.AppCore.Repositories
{
	public interface ICompanyRepository
	{
		CompanyEntityModel GetCompany();
		bool UpdateCompanyDetails(CompanyEntityModel companyEntityModel);
	}
}
