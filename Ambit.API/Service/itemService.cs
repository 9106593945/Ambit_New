using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Ambit.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Navrang.Services
{
	public class itemService : IitemService
	{
		private readonly AppSettings _appSettings;
		private readonly IRepoSupervisor _repoSupervisor;
		public itemService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
		{
			_appSettings = appSettings.Value;
			_repoSupervisor = repoSupervisor;
		}

		public IEnumerable<ItemEntityModel> GetAllItems(int categoryid, int customerid, int customerLoginId)
		{
			return _repoSupervisor.Items.GetAllItems(categoryid, customerid, customerLoginId);
		}

		public IEnumerable<ItemEntityModel> GetAllItemsBySearchCrieteria(JDatatableParameters searchParams, out int TotalCount)
		{
			return _repoSupervisor.Items.GetAllItemsBySearchCrieteria(searchParams, out TotalCount);
		}

		public IEnumerable<ItemEntityModel> GetAllItemByCategory(int categoryId, int customerId, int customerLoginId)
		{
			return _repoSupervisor.Items.GetAllItemByCategory(categoryId, customerId, customerLoginId);
		}

		public bool ClearAllFavorite(int customerId)
		{
			if (_repoSupervisor.Items.ClearAllFavorite(customerId))
			{
				_repoSupervisor.Complete();
			}
			return true;
		}

		public IEnumerable<ItemEntityModel> GetAllFavoriteItem(int customerId)
		{
			var favoriteItems = _repoSupervisor.Items.GetAllFavoriteItem(customerId);
			favoriteItems = favoriteItems
						.Select(c => { c.ImagePath = _appSettings.SiteUrl + "/images/items/resize/" + c.Image; return c; })
						.Where(a => a.Active == true)
						.ToList();
			return favoriteItems;
		}
		public bool UpsertFavoriteItem(int customerId, int itemId, bool isFavourite)
		{
			return _repoSupervisor.Items.UpsertFavoriteItem(customerId, itemId, isFavourite);
		}

		public ItemEntityModel GetItemByID(int Id, int customerId = 0)
		{
			var item = _repoSupervisor.Items.GetItemById(Id, customerId);
			return item;
		}

		public bool IsItemCodeExist(string itemCode, int itemId = 0)
		{
			if (_repoSupervisor.Items.IsItemCodeExist(itemCode, itemId))
			{
				return true;
			}
			return false;
		}

		public bool DeleteItem(long id)
		{
			if (_repoSupervisor.Items.DeleteItem(id))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public bool ActiveInactiveItem(long id, bool status)
		{
			if (_repoSupervisor.Items.ActiveInactiveItem(id, status))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public IEnumerable<ItemEntityModel> GetItemsByKey(string key)
		{
			return _repoSupervisor.Items.GetItemsByKey(key);
		}


        public IEnumerable<CategoryEntityModel> GetAllCategory()
        {
            var favoriteItems = _repoSupervisor.Items.GetAllCategory();
            favoriteItems = favoriteItems
                        .Select(c => { c.ImagePath = _appSettings.SiteUrl + "/images/items/resize/" + c.Image; return c; })
                        .Where(a => a.Active == true)
                        .ToList();
            return favoriteItems;
        }
    }
}
