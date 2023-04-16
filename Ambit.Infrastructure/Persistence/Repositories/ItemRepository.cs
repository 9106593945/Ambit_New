using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace Ambit.Infrastructure.Persistence.Repositories
{
	public class ItemRepository : BaseRepository, IItemRepository
	{
		private readonly IDapper _dapper;
		public ItemRepository(AppDbContext dbContext, IDapper dapper) : base(dbContext)
		{
			_dapper = dapper;
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public IEnumerable<ItemEntityModel> GetAllItems(int categoryid, int customerid, int customerLoginId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("categoryid", categoryid);
			parameters.Add("customerid", customerid);
			parameters.Add("customerLoginId", customerLoginId);

			var Items = _dapper.GetAll<ItemEntityModel>($"exec [ItemsSelectAll] @categoryid=@categoryid, @customerId = @customerId, @customerLoginId= @customerLoginId", parameters, commandType: CommandType.Text);

			if (Items == null)
				return null;

			return Items;
		}
		public IEnumerable<ItemEntityModel> AllItems()
		{
			return _dbContext.Items.Where(s => s.isDeleted == false).Select(a => new ItemEntityModel
			{
				Name = a.name,
				ItemId = a.itemid,
				Code = a.code
			});
		}

		public IEnumerable<ItemEntityModel> GetAllItemsBySearchCrieteria(JDatatableParameters searchParams, out int TotalCount)
		{
			TotalCount = 0;
			if (searchParams != null)
			{
				var parameters = new DynamicParameters();
				parameters.Add("searchText", searchParams.SearchText);
				parameters.Add("sortColumn", searchParams.OrderByCriteria ?? "itemid");
				parameters.Add("sortDirection", searchParams.OrderByDirection ?? "DESC");
				parameters.Add("startIndex", searchParams.Start);
				parameters.Add("recordsPerPage", searchParams.Length);

				var Items = _dapper.GetAll<ItemEntityModel>($"exec [getAllItems] @searchText = @searchText, @sortColumn = @sortColumn, @sortDirection = @sortDirection, @startIndex = @startIndex, @recordsPerPage = @recordsPerPage", parameters, commandType: CommandType.Text);

				if (Items != null)
				{
					TotalCount = Items.Count();

					var ItemsWithOrder = Items.AsQueryable();//.OrderBy((searchParams.OrderByCriteria ?? "itemid") + " " + (searchParams.OrderByDirection ?? "DESC")).ToList();
					return ItemsWithOrder.Skip(searchParams.Start).Take(searchParams.Length).ToList();
				}
			}
			return null;
		}

		public IEnumerable<ItemEntityModel> GetAllItemByCategory(int categoryId, int customerId, int customerLoginId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("customerId", customerId);
			parameters.Add("categoryId", categoryId);
			parameters.Add("customerLoginId", customerLoginId);
			var Items = _dapper.GetAll<ItemEntityModel>($"exec [getAllItemsByCategory] @customerId = @customerId, @categoryId=@categoryId, @customerLoginId = @customerLoginId", parameters, commandType: CommandType.Text);

			if (Items == null)
				return null;

			return Items;
		}

		public IEnumerable<ItemEntityModel> GetAllFavoriteItem(int customerId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("customerId", customerId);

			var Items = _dapper.GetAll<ItemEntityModel>($"exec [GetAllFavoriteItem] @customerId = @customerId", parameters, commandType: CommandType.Text);

			if (Items == null)
				return null;

			return Items;
		}

		public bool UpsertFavoriteItem(int customerId, int itemId, bool isFavourite)
		{
			var parameters = new DynamicParameters();
			parameters.Add("customerId", customerId);
			parameters.Add("itemId", itemId);
			parameters.Add("isFavourite", isFavourite);

			var FavoriteItem = _dapper.GetAll<int>($"exec [UpsertFavoriteItem] @customerid = @customerId,@itemid=@itemId,@isfavorite = @isFavourite ", parameters, commandType: CommandType.Text).FirstOrDefault();

			return FavoriteItem > 0;
		}

		public ItemEntityModel GetItemById(int Id, int customerId)
		{

			var parameters = new DynamicParameters();
			parameters.Add("customerId", customerId);
			parameters.Add("id", Id);

			var item = _dapper.Get<ItemEntityModel>($"exec [GetItemById] @id=@id, @customerid = @customerId ", parameters, commandType: CommandType.Text);

			//var Items = _dbContext.ViewAllItems.ToList()
			//	.Where(i => i.isDeleted == false && i.Itemid == Id)
			//	.Select(x => new ItemEntityModel()
			//	{
			//		Description = x.Description,
			//		Supplier = x.Supplier,
			//		Code = x.Code,
			//		ItemId = x.Itemid,
			//		Name = x.Name,
			//		Discount = x.Discount,
			//		OpeningQuantity = x.OpeningQuantity,
			//		PurchaseAmount = x.PurchaseAmount,
			//		SellAmount = x.SellAmount,
			//		Active = x.Active,
			//		Image = x.Image,
			//		CategoryIds = x.categoryIds,
			//		Stock = x.Stock
			//	});

			if (item == null)
				return null;

			return item;
		}
		public ItemEntityModel GetItemByName(string name)
		{
			var Items = _dbContext.ViewAllItems.ToList()
				.Where(i => i.isDeleted == false && (i.Name == name || i.Code + " - " + i.Name == name))
				.Select(x => new ItemEntityModel()
				{
					Description = x.Description,
					Supplier = x.Supplier,
					Code = x.Code,
					ItemId = x.Itemid,
					Name = x.Name,
					Discount = x.Discount,
					OpeningQuantity = x.OpeningQuantity,
					PurchaseAmount = x.PurchaseAmount,
					SellAmount = x.SellAmount,
					Active = x.Active,
					Image = x.Image,
					CategoryIds = x.categoryIds,
					ShownInApp = x.ShownInApp,
					Stock = x.Stock,
				});

			if (Items == null)
				return null;

			return Items.FirstOrDefault();
		}

		public EntityEntry<Items> AddNewItem(ItemEntityModel itemEntityModel)
		{
			var Item = _dbContext.Items.Add(new Items
			{
				code = itemEntityModel.Code,
				name = itemEntityModel.Name,
				description = itemEntityModel.Description,
				supplier = itemEntityModel.Supplier,
				purchaseamount = itemEntityModel.PurchaseAmount,
				sellamount = itemEntityModel.SellAmount,
				retailamount = itemEntityModel.RetailAmount,
				mrp = itemEntityModel.MRP,
				discount = itemEntityModel.Discount ?? 0,
				openingquantity = itemEntityModel.OpeningQuantity,
				image = itemEntityModel.Image,
				showninapp = itemEntityModel.ShownInApp,
				IsComboProduct = itemEntityModel.IsComboProduct,
			});

			return Item;
		}

		public bool UpsertItemCategory(long itemId, string categoryIds)
		{
			var parameters = new DynamicParameters();
			parameters.Add("categoryIds", categoryIds);
			parameters.Add("itemId", itemId);

			var FavoriteItem = _dapper.GetAll<int>($"exec [UpsertItemCategory] @itemid=@itemId,@categoryIds = @categoryIds ", parameters, commandType: CommandType.Text).FirstOrDefault();

			return FavoriteItem > 0;
		}

		public bool UpdateItem(ItemEntityModel itemEntityModel)
		{
			var Item = _dbContext.Items.Find(itemEntityModel.ItemId);
			if (Item != null)
			{
				Item.name = itemEntityModel.Name;
				Item.code = itemEntityModel.Code;
				Item.description = itemEntityModel.Description;
				Item.supplier = itemEntityModel.Supplier;
				Item.purchaseamount = itemEntityModel.PurchaseAmount;
				Item.sellamount = itemEntityModel.SellAmount;
				Item.retailamount = itemEntityModel.RetailAmount;
				Item.mrp = itemEntityModel.MRP;
				Item.discount = itemEntityModel.Discount ?? 0;
				Item.openingquantity = itemEntityModel.OpeningQuantity;
				Item.image = itemEntityModel.Image;
				Item.showninapp = itemEntityModel.ShownInApp;

				return true;
			}
			return false;
		}

		public bool IsItemCodeExist(string itemCode, int itemId = 0)
		{
			var Item = _dbContext.Items.Where(i => i.code.ToUpper() == itemCode.ToUpper() && i.itemid != itemId && i.isDeleted == false);
			if (Item != null && Item.Count() > 0)
			{
				return true;
			}
			return false;
		}

		public bool DeleteItem(long id)
		{
			var Item = _dbContext.Items.Find(id);
			if (Item != null)
			{
				Item.isDeleted = true;
				return true;
			}
			return false;
		}

		public bool ActiveInactiveItem(long id, bool status)
		{
			var Item = _dbContext.Items.Find(id);
			if (Item != null)
			{
				Item.Active = status;
				return true;
			}
			return false;
		}

		public IEnumerable<ItemEntityModel> GetItemsByKey(string key)
		{
			var Item = _dbContext.Items.Where(i => i.name.Contains(key) && i.isDeleted == false && i.Active == true)
				.Select(x => new ItemEntityModel()
				{
					Description = x.description,
					Supplier = x.supplier,
					Code = x.code,
					ItemId = x.itemid,
					Name = x.name,
					Discount = x.discount,
					OpeningQuantity = x.openingquantity,
					PurchaseAmount = x.purchaseamount,
					SellAmount = x.sellamount,
					ShownInApp = x.showninapp,
					Image = x.image
				});
			return Item;
		}

		public IEnumerable<CategoryEntityModel> GetAllCategory()
		{
			var parameters = new DynamicParameters();

			var Items = _dapper.GetAll<CategoryEntityModel>($"exec [CategorySelectAll]", parameters, commandType: CommandType.Text);

			if (Items == null)
				return null;

			return Items;
		}

	}
}
