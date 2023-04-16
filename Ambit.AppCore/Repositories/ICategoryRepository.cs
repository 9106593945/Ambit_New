using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Repositories
{
	public interface ICategoryRepository
	{
		IEnumerable<CategoryEntityModel> GetAllCategories();
		CategoryEntityModel GetCategoryById(int Id);
		bool UpdateCategory(CategoryEntityModel CategoryEntityModel);
		bool AddNewCategory(CategoryEntityModel CategoryEntityModel);
		bool DeleteCategory(long id);
		bool ActiveInactiveCategory(long id, bool status);
		IEnumerable<CategoryEntityModel> GetCategoriesByKey(string key);
	}
}
