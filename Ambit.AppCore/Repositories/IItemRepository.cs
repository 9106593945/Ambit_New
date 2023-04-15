using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using System.Collections.Generic;

namespace Ambit.AppCore.Repositories
{
	public interface IItemRepository
	{
		IEnumerable<ItemEntityModel> GetAllItems(int categoryid, int customerid, int customerLoginId);
		IEnumerable<ItemEntityModel> GetAllItemsBySearchCrieteria(JDatatableParameters searchParams, out int TotalCount);
		ItemEntityModel GetItemById(int Id, int customerId);
		bool UpdateItem(ItemEntityModel itemEntityModel);
		EntityEntry<Items> AddNewItem(ItemEntityModel itemEntityModel);
		bool IsItemCodeExist(string itemCode, int itemId = 0);
		bool DeleteItem(long id);
		bool ActiveInactiveItem(long id, bool status);
		IEnumerable<ItemEntityModel> GetItemsByKey(string key);
		ItemEntityModel GetItemByName(string name);
		IEnumerable<ItemEntityModel> GetAllItemByCategory(int categoryId, int customerId, int customerLoginId);
		IEnumerable<ItemEntityModel> GetAllFavoriteItem(int customerId);
		bool ClearAllFavorite(int customerId);
		bool UpsertFavoriteItem(int customerId, int itemId, bool isFavourite);
		bool UpsertItemCategory(long itemId, string categoryIds);
		IEnumerable<CategoryEntityModel> GetAllCategory();
    }
}
