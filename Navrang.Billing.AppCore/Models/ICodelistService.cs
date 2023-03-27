using Navrang.Billing.AppCore.EntityModels;
using System.Collections.Generic;

namespace Navrang.Billing.AppCore.Models
{
	public interface ICodelistService
	{
		List<CodelistEntityModel> GetCitiesByKey(string key);
		List<CodelistEntityModel> GetStatesByKey(string key);
	}
}
