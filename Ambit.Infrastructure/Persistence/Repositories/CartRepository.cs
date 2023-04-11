using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ambit.Infrastructure.Persistence.Repositories
{
	public class CartRepository : BaseRepository, ICartRepository
	{
		private readonly IDapper _dapper;
		public CartRepository(AppDbContext dbContext, IDapper dapper) : base(dbContext)
		{
			_dapper = dapper;
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public IEnumerable<CartEntityModel> GetAllCarts()
		{
			var Carts = _dbContext.Cart.Where(a => a.isDeleted == false).Select(x => new CartEntityModel()
			{
				cartid = x.cartid,
				//Cart_Number = x.cartnumber,
				//Cart_Date = x.cartdate,
				//Customer_Name = x.customername,
				//SubTotal = x.amount,
				//CustomerId = x.customerid,
				//Total_Amount = x.total_amount
			})
				.OrderByDescending(o => o.cartid);
			if (Carts == null)
				return null;

			return Carts;
		}

		public CartEntityModel GetCartById(int Id)
		{
			var Carts = _dbContext.Cart
				.Where(a => a.isDeleted == false && a.cartid == Id)
				.Select(x => new CartEntityModel()
				{
					cartid = x.cartid,
					//Cart_Number = x.cartnumber,
					//Cart_Date = x.cartdate,
					//CustomerId = x.customerid,
					//Customer_Name = x.customername,
					//SubTotal = x.amount,
					//Total_Amount = x.total_amount,
					Active = x.Active,
					//Description = x.description,
					//Extra = x.extra,
					//ShippingCharge = x.shippingcharge ?? 0,
					//TaxPercent = x.taxpercent ?? 0,
					//TaxAmount = x.taxamount ?? 0
				})
				.FirstOrDefault();

			if (Carts == null)
				return null;

			return Carts;
		}

		public List<CartItemEntityModel> GetCartItemsByCartId(int Id)
		{
			var CartItems = _dbContext.CartItems
				.Where(a => a.isDeleted == false && a.cartid == Id)
				.Select(x => new CartItemEntityModel()
				{})
				.ToList();

			if (CartItems == null)
				return null;

			return CartItems;
		}

		public EntityEntry<cart> AddNewCart(CartEntityModel CartEntity)
		{
			var Cart = _dbContext.Cart.Add(new cart
			{
				Active = CartEntity.Active,
				isDeleted = CartEntity.isDeleted,
				Created_On = CartEntity.Created_On,
				Created_By = CartEntity.Created_By,
				Updated_On = CartEntity.Updated_On,
				Updated_By = CartEntity.Updated_By,
				customerloginid = CartEntity.customerloginid,
				customername = "",

			});


			return Cart;
		}

		public EntityEntry<cartitems> AddCartItems(CartItemEntityModel CartItem)
		{
			var CartItems = _dbContext.CartItems.Add(new cartitems
			{
				itemid = CartItem.itemid,
				cartid = CartItem.cartid,
				quantity = CartItem.quantity,
				Active = CartItem.Active,
                isDeleted = CartItem.isDeleted,
                Created_On = CartItem.Created_On,
                Created_By = CartItem.Created_By,
                Updated_On = CartItem.Updated_On,
                Updated_By = CartItem.Updated_By,
                customerloginid = CartItem.customerloginid,
			});

			return CartItems;
		}

		public bool UpdateCartNumber(long CartId, string CartNumber)
		{
			var Cart = _dbContext.Cart.Find(CartId);
			//if (Cart != null)
			//{
			//	Cart.cartnumber = CartNumber;
			//}
			return true;
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

		public bool UpdateCartItems(CartItemEntityModel CartItemEntity)
		{
			var CartItem = _dbContext.CartItems.Find(CartItemEntity.cartid);
			if (CartItem != null)
			{
				//CartItem.itemid = CartItemEntity.ItemId;
				//CartItem.image = CartItemEntity.Image;
				//CartItem.quantity = CartItemEntity.Quantity;
				//CartItem.name = CartItemEntity.Name;
				//CartItem.price = CartItemEntity.SellingPrice;
				//CartItem.totalamount = CartItemEntity.Amount;
				return true;
			}
			return false;
		}

		public bool DeleteCartItems(long id)
		{
			var CartItem = _dbContext.CartItems.Find(id);
			if (CartItem != null)
			{
				CartItem.isDeleted = true;
				return true;
			}
			return false;
		}

		public bool IsCartCodeExist(string CartCode)
		{
			var Cart = _dbContext.Cart.Where(i => i.isDeleted == false);
			if (Cart != null && Cart.Count() > 0)
			{
				return true;
			}
			return false;
		}

		public bool DeleteCart(long id)
		{
			var Cart = _dbContext.Cart.Find(id);
			if (Cart != null)
			{
				Cart.isDeleted = true;
				return true;
			}
			return false;
		}

		public bool DeleteCartItemsByCartId(long id)
		{
			var Cart = _dbContext.CartItems.Where(i => i.id == id && i.isDeleted == false);
			if (Cart != null)
			{
				Cart.ToList().ForEach(c => c.isDeleted = true);
				return true;
			}
			return false;
		}
		public bool DeleteCartItemsByCartItemId(long id)
		{
			var Cart = _dbContext.CartItems.Where(i => i.cartid == id && i.isDeleted == false);
			if (Cart != null)
			{
				Cart.ToList().ForEach(c => c.isDeleted = true);
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

		public CartEntityModel GetLastCart()
		{
			var Cart = _dbContext.Cart.Where(a => a.isDeleted == false)
				.OrderByDescending(o => o.cartid)
				.Select(x => new CartEntityModel()
				{
					//CartId = x.cartid,
					//Cart_Number = x.cartnumber,
					//Cart_Date = x.cartdate,
					//CustomerId = x.customerid,
					//Customer_Name = x.customername,
					//SubTotal = x.amount,
					//Total_Amount = x.total_amount,
					Active = x.Active,
					Created_By = x.Created_By,
					//Created_On = x.Created_On
				})
				.FirstOrDefault();

			if (Cart == null)
				return null;

			return Cart;
		}

		public List<ItemEntityModel> GetSerchItems(string term)
		{
			//var parameters = new DynamicParameters();
			//parameters.Add("term", term);
			//parameters.Add("customerId", customerId);

			//var query = "exec [GetSearchItem] @term = @term, @customerid = @customerId";
			//var items = _dapper.GetAll<ItemEntityModel>(query, parameters, commandType: CommandType.Text);

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
					    })
					    .ToList();

			return items;
		}

		public List<CartItemEntityModel> getCustomerCartDetailsById(int customerloginid)
		{
			try
			{
                IQueryable<CartItemEntityModel> CustomerCarts = from st in _dbContext.CartItems
                                                                join s in _dbContext.Cart on st.cartid equals s.cartid
                                                                where s.isDeleted == false && st.isDeleted == false && st.customerloginid == customerloginid
                                                                select new CartItemEntityModel
                                                                {
                                                                    Active = st.Active,
                                                                    cartid = st.cartid,
                                                                    itemid = st.itemid,
                                                                    quantity = st.quantity,
                                                                    isDeleted = st.isDeleted,
                                                                    Created_On = st.Created_On,
																	id = st.id
                                                                };
                return CustomerCarts.ToList();
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
                var Cart = _dbContext.Cart.Where(i => i.customerloginid == customerloginid);
                if (Cart != null && Cart.Count() > 0)
                {
                    return Convert.ToInt32(Cart.FirstOrDefault().cartid);
                }
                return 0;
            }
			catch (Exception ex)
			{
				throw;
			}
            
        }

    }
}
