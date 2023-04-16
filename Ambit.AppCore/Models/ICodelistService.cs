using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Models
{
	public interface ICodelistService
	{
		List<CodelistEntityModel> GetCitiesByKey(string key);
		List<CodelistEntityModel> GetStatesByKey(string key);
	}
}
