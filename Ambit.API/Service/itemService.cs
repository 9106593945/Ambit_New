using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Microsoft.Extensions.Options;

namespace Ambit.Services
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

		public IEnumerable<ItemEntityModel> GetAllItems(CategoryItemRequest request)
		{
			return _repoSupervisor.Items.GetAllItems(request);
		}

		public IEnumerable<ItemEntityModel> GetAllItemsBySearchCrieteria(JDatatableParameters searchParams, out int TotalCount)
		{
			return _repoSupervisor.Items.GetAllItemsBySearchCrieteria(searchParams, out TotalCount);
		}

		public IEnumerable<ItemEntityModel> GetAllItemByCategory(int categoryId, int customerId, int customerLoginId)
		{
			return _repoSupervisor.Items.GetAllItemByCategory(categoryId, customerId, customerLoginId);
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
			var category = _repoSupervisor.Items.GetAllCategory();
			category = category
					  .Select(c => { c.ImagePath = _appSettings.SiteUrl + "/images/category/resize/" + c.ImagePath; return c; })
					  .Where(a => a.Active == true)
					  .ToList();
			return category;
		}
	}
}
