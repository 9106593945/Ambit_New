using Microsoft.AspNetCore.Http;
using Ambit.AppCore.EntityModels;
using System.Collections.Generic;

namespace Ambit.AppCore.Models
{
	public interface IitemService 
	{
		IEnumerable<ItemEntityModel> GetAllItems();
		IEnumerable<ItemEntityModel> GetAllItemsBySearchCrieteria(JDatatableParameters searchParams, out int TotalCount);

		ItemEntityModel GetItemByID(int Id,int customerId = 0);
		bool DeleteItem(long id);
		bool ActiveInactiveItem(long id, bool status);
		bool IsItemCodeExist(string itemCode, int itemId = 0);
		IEnumerable<ItemEntityModel> GetItemsByKey(string key);
		IEnumerable<ItemEntityModel> GetAllItemByCategory(int categoryId, int customerId, int customerLoginId);
		IEnumerable<ItemEntityModel> GetAllFavoriteItem(int customerId);
		bool UpsertFavoriteItem(int customerId, int itemId, bool isFavourite);
		bool ClearAllFavorite(int customerId);
	}
}
