using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Models
{
	public interface ICompanyService
	{
		CompanyEntityModel GetCompanyDetails();
		bool UpdateCompanyDetails(CompanyEntityModel companyEntityModel);
	}
}
