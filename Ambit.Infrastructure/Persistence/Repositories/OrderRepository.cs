using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ambit.Infrastructure.Persistence.Repositories
{
	public class OrderRepository : BaseRepository, IOrderRepository
	{
		private readonly IDapper _dapper;
		public OrderRepository(AppDbContext dbContext, IDapper dapper) : base(dbContext)
		{
			_dapper = dapper;
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public List<CartItemEntityModel> GetCartItemsByCartId(int Id)
		{
			var CartItems = _dbContext.CartItems
				.Where(a => a.isDeleted == false && a.cartid == Id)
				.Select(x => new CartItemEntityModel()
				{ })
				.ToList();

			if (CartItems == null)
				return null;

			return CartItems;
		}

		public EntityEntry<cart> AddNewCart(CartEntityModel CartEntity)
		{
			var Cart = _dbContext.Cart.Add(new cart
			{
				customerloginid = CartEntity.customerloginid,
				customername = "",
			});
			return Cart;
		}

		public EntityEntry<cartitems> AddCartItems(CartItemEntityModel CartItem)
		{
			var CartItems = _dbContext.CartItems.Add(new cartitems
			{
				itemid = CartItem.ItemId,
				cartid = CartItem.CartId,
				quantity = CartItem.Quantity,
				Active = CartItem.Active,
				isDeleted = CartItem.IsDeleted,
				Created_On = CartItem.Created_On,
				Created_By = CartItem.Created_By,
				Updated_On = CartItem.Updated_On,
				Updated_By = CartItem.Updated_By,
				customerloginid = CartItem.CustomerLoginId,
			});

			return CartItems;
		}

		public bool UpdateCart(CartEntityModel CartEntity)
		{
			var Cart = _dbContext.Cart.Find(CartEntity.cartid);
			if (Cart != null)
			{
				//Cart.description = CartEntity.Description;
				//Cart.total_amount = CartEntity.Total_Amount;
				//Cart.extra = CartEntity.Extra;
				//Cart.shippingcharge = CartEntity.ShippingCharge;
				//Cart.taxpercent = CartEntity.TaxPercent;
				//Cart.taxamount = CartEntity.TaxAmount;
				//Cart.cartdate = CartEntity.Cart_Date;
				//Cart.customerid = CartEntity.CustomerId;
				//Cart.customername = CartEntity.Customer_Name;
				return true;
			}
			return false;
		}

		public bool UpdateCartItems(CartItemEntityModel request)
		{
			var CartItem = _dbContext.CartItems.First(s => s.cartid == request.CartId && s.itemid == request.ItemId && s.isDeleted == false);
			if (CartItem != null)
			{
				CartItem.quantity = request.Quantity;
				return true;
			}
			return false;
		}

		public bool DeleteCartItems(long itemId, int customerLoginId)
		{
			var CartItem = _dbContext.CartItems.First(s => s.isDeleted == false && s.customerloginid == customerLoginId && s.itemid == itemId);
			if (CartItem != null)
			{
				CartItem.isDeleted = true;
				return true;
			}
			return false;
		}

		public bool DeleteCart(long customerLoginId)
		{
			var Cart = _dbContext.Cart.First(s => s.customerloginid == customerLoginId && s.isDeleted == false);
			if (Cart != null)
			{
				Cart.isDeleted = true;
				return true;
			}
			return false;
		}
		public bool DeleteCartItems(long customerLoginId)
		{
			var Cart = _dbContext.CartItems.Where(i => i.customerloginid == customerLoginId && i.isDeleted == false);
			if (Cart != null)
			{
				Cart.ForEachAsync(c => c.isDeleted = true);
				return true;
			}
			return false;
		}

		public bool ActiveInactiveCart(long id, bool status)
		{
			var Cart = _dbContext.Cart.Find(id);
			if (Cart != null)
			{
				Cart.Active = status;
				return true;
			}
			return false;
		}

		public List<ItemEntityModel> GetSerchItems(string term)
		{
			var items = _dbContext.Items
						.Where(i => i.isDeleted == false && i.Active == true && (i.name.Contains(term) || i.code.Contains(term)))
						.Select(s => new ItemEntityModel()
						{
							Code = s.code,
							Image = s.image,
							Discount = s.discount,
							Name = s.name,
							ItemId = s.itemid,
							SellAmount = s.sellamount,
							PurchaseAmount = s.purchaseamount,
							OpeningQuantity = s.openingquantity,
							Description = s.description
						}).AsNoTracking()
						.ToList();

			return items;
		}

		public List<CartItemEntityModel> GetCartDetailsByCustomerLoginId(int customerloginid)
		{
			try
			{
				var cartDetail = from st in _dbContext.CartItems
							  join s in _dbContext.Cart on st.cartid equals s.cartid
							  where s.isDeleted == false && st.isDeleted == false && st.customerloginid == customerloginid
							  select new CartItemEntityModel
							  {
								  Active = st.Active ?? true,
								  CartId = st.cartid,
								  ItemId = st.itemid,
								  Quantity = st.quantity,
								  Created_On = st.Created_On,
								  Id = st.id
							  };
				return cartDetail.ToList();
			}
			catch (Exception ex)
			{

				throw;
			}

		}

		public decimal getCustomerTotalAmountById(long customerId)
		{
			var totalAmount = _dbContext.Cart
				.Where(i => i.isDeleted == false)
				.Sum(s => s.cartid);
			return totalAmount;
		}
		public int IsCartExist(int customerloginid)
		{
			try
			{
				var Cart = _dbContext.Cart.First(i => i.customerloginid == customerloginid && i.isDeleted == false);
				if (Cart != null)
				{
					return Cart.cartid;
				}
				return 0;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}

	}
}
