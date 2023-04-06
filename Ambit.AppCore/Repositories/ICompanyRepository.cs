using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Repositories
{
	public interface ICompanyRepository
	{
		CompanyEntityModel GetCompany();
		bool UpdateCompanyDetails(CompanyEntityModel companyEntityModel);
	}
}
