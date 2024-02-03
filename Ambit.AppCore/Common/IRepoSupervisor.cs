using Ambit.AppCore.Repositories;

namespace Ambit.AppCore.Common
{
	public interface IRepoSupervisor
	{
		ILoginRepository Logins { get; }
		IItemRepository Items { get; }
		ICategoryRepository Category { get; }
		ICustomerRepository Customer { get; }
		ICompanyRepository Company { get; }
		ICartRepository Cart { get; }
		IOrderRepository Order { get; }
		IHomeRepository Home { get; }
		IBannerRepository Banner { get; }
		ICodelistRepository Codelist { get; }
		int Complete(string username = null);

	}
}
