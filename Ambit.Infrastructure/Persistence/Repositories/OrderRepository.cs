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

		public EntityEntry<Order> AddOrder(OrderEntityModel orderEntityModel)
		{
			var Order = _dbContext.Order.Add(new Order
            {
                CustomerId = orderEntityModel.CustomerId,
                ordernumber = orderEntityModel.ordernumber,
                description = orderEntityModel.description,
                customername = orderEntityModel.customername,
                status = orderEntityModel.status,
                subtotal = orderEntityModel.subtotal,
                tax = orderEntityModel.tax,
                orderdate = orderEntityModel.orderdate,
                discount = orderEntityModel.discount,
                shipingcharge = orderEntityModel.shipingcharge,
                grandtotal = orderEntityModel.grandtotal,
                shippingaddressid = orderEntityModel.shippingaddressid,
                billingaddressid = orderEntityModel.billingaddressid,
                Active = true,
                isDeleted = orderEntityModel.IsDeleted,
                Created_On = orderEntityModel.Created_On,
                Created_By = orderEntityModel.Created_By,
                Updated_On = orderEntityModel.Updated_On,
                Updated_By = orderEntityModel.Updated_By
            });
			return Order;
		}

		public EntityEntry<OrderItems> AddOrderItems(OrderItemEntityModel OrderItemEntityModel)
        {
			var OrderItems = _dbContext.OrderItems.Add(new OrderItems
            {
                orderid = OrderItemEntityModel.OrderId,
                itemid = OrderItemEntityModel.ItemId,
                quantity = OrderItemEntityModel.Quantity,
                price = OrderItemEntityModel.Price,
                tax = OrderItemEntityModel.Tax,
                total = OrderItemEntityModel.Total,
                customerid = OrderItemEntityModel.CustomerId,
                customername = OrderItemEntityModel.customername
            });

			return OrderItems;
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

		public List<OrderEntityModel> GetOrderDetailsByCustomerLoginId(int customerloginid)
		{
			try
			{
				var orderDetail = from st in _dbContext.Order
							 // join s in _dbContext.Cart on st.cartid equals s.cartid
							  where st.CustomerId == customerloginid
                                  select new OrderEntityModel
                                  {
                                      orderid = st.orderid,
                                      ordernumber = st.ordernumber,
                                      description = st.description,
                                      customername = st.customername,
                                      CustomerId = st.CustomerId,
                                      status = st.status,
                                      subtotal = st.subtotal,
                                      tax = st.tax,
                                      orderdate = st.orderdate,
                                      discount = st.discount,
                                      shipingcharge = st.shipingcharge,
                                      grandtotal = st.grandtotal,
                                      shippingaddressid = st.shippingaddressid,
                                      billingaddressid = st.billingaddressid,
                                      Active = st.Active
                                  };
                return orderDetail.ToList();
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

        public OrderEntityModel GetByOrderId(int orderId)
        {
            try
            {
                var orderDetail = from st in _dbContext.Order
                                      // join s in _dbContext.Cart on st.cartid equals s.cartid
                                  where st.orderid == orderId
                                  select new OrderEntityModel
                                  {
                                      orderid = st.orderid,
                                      ordernumber = st.ordernumber,
                                      description = st.description,
                                      customername = st.customername,
                                      CustomerId = st.CustomerId,
                                      status = st.status,
                                      subtotal = st.subtotal,
                                      tax = st.tax,
                                      orderdate = st.orderdate,
                                      discount = st.discount,
                                      shipingcharge = st.shipingcharge,
                                      grandtotal = st.grandtotal,
                                      shippingaddressid = st.shippingaddressid,
                                      billingaddressid = st.billingaddressid,
                                      Active = st.Active
                                  };
                return orderDetail.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
		public List<OrderItemEntityModel> GetOrderItemsById(int orderId)
        {
            try
            {
                var orderDetail = from st in _dbContext.OrderItems
                                      // join s in _dbContext.Cart on st.cartid equals s.cartid
                                  where st.orderid == orderId
                                  select new OrderItemEntityModel
                                  {
                                      Active = st.Active ?? true,
                                      OrderId = st.orderid,
                                      ItemId = st.itemid,
                                      Quantity = st.quantity,
                                      Price = st.price,
                                      Tax = st.tax,
                                      Total = st.total,
                                      Created_On = st.Created_On,
                                      Id = st.id
                                  };
                return orderDetail.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
