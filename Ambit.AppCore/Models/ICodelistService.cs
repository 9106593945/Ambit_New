using Ambit.AppCore.EntityModels;
using System.Collections.Generic;

namespace Ambit.AppCore.Models
{
	public interface ICodelistService
	{
		List<CodelistEntityModel> GetCitiesByKey(string key);
		List<CodelistEntityModel> GetStatesByKey(string key);
	}
}
