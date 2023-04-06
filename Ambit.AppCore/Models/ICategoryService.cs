using Ambit.AppCore.EntityModels;
using System.Collections.Generic;

namespace Ambit.AppCore.Models
{
	public interface ICategoryservice
	{
		IEnumerable<CategoryEntityModel> GetAllCategories();
		CategoryEntityModel GetCategoryByID(int Id);
		bool AddNewCategory(CategoryEntityModel CategoryEntityModel);
		bool UpdateCategory(CategoryEntityModel CategoryEntityModel);
		bool DeleteCategory(long id);
		bool ActiveInactiveCategory(long id, bool status);
		IEnumerable<CategoryEntityModel> GetCategoriesByKey(string key);
	}
}
