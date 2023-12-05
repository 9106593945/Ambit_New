using Ambit.AppCore.EntityModels;

namespace Ambit.AppCore.Models
{
	public interface IitemService
	{
		IEnumerable<ItemEntityModel> GetAllItems(CategoryItemRequest request);
		IEnumerable<ItemEntityModel> GetAllItemsBySearchCrieteria(JDatatableParameters searchParams, out int TotalCount);

		ItemEntityModel GetItemByID(int Id, int customerId = 0);
		bool DeleteItem(long id);
		bool ActiveInactiveItem(long id, bool status);
		bool IsItemCodeExist(string itemCode, int itemId = 0);
		IEnumerable<ItemEntityModel> GetItemsByKey(string key);
		IEnumerable<ItemEntityModel> GetAllItemByCategory(int categoryId, int customerId, int customerLoginId);
		IEnumerable<CategoryEntityModel> GetAllCategory();

	}
}
