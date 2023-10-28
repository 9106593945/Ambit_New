using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Ambit.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ambit.Services
{
	public class CartService : ICartService
	{
		private readonly AppSettings _appSettings;
		private readonly IRepoSupervisor _repoSupervisor;
		public CartService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
		{
			_appSettings = appSettings.Value;
			_repoSupervisor = repoSupervisor;
		}

		public IEnumerable<CartEntityModel> GetCartList()
		{
			return _repoSupervisor.Cart.GetAllCarts();
		}

		public CartEntityModel GetCartByID(int Id)
		{
			CartEntityModel CartEntity = new CartEntityModel();
			CartEntity = _repoSupervisor.Cart.GetCartById(Id);
			CartEntity.CartItems = _repoSupervisor.Cart.GetCartItemsByCartId(Id);
			return CartEntity;
		}

		public Int64 AddNewCart(CartEntityModel CartEntityModel, IFormCollection collection)
		{
			try
			{
				//if (IsCartCodeExist(CartEntityModel.Cart_Number))
				//{
				//	return 0;
				//}
				//if (CartEntityModel.CustomerId <= 0)
				//{
				//	_repoSupervisor.Customer.AddNewCustomer(new CustomerEntityModel()
				//	{
				//		Name = CartEntityModel.Customer_Name
				//	});
				//	_repoSupervisor.Complete();

				//}
				var Cart = _repoSupervisor.Cart.AddNewCart(CartEntityModel);
				if (Cart != null)
				{
					_repoSupervisor.Complete();
					int TotalItems = 0;
					long itemId = 0;
					if (Cart.Entity.cartid > 0)
					{
						int.TryParse(collection["hdnTotalItems"], out TotalItems);
						string itemName = "", itemImage = "", itemAction = "", itemCode = "";
						decimal itemAmount = 0, itemPrice = 0;
						int itemQty = 0;
						for (int intI = 1; intI <= TotalItems; intI++)
						{
							itemAction = Convert.ToString(collection["itemAction_" + intI]);
							itemCode = Convert.ToString(collection["itemCode_" + intI]);
							long.TryParse(Convert.ToString(collection["itemId_" + intI]), out itemId);
							if (itemAction == "DA")
							{
								continue;
							}
							if (itemAction == "A")
							{
								itemName = Convert.ToString(collection["txtItem_" + intI]);
								itemQty = Convert.ToInt32(collection["quantity_" + intI]);
								itemPrice = Convert.ToDecimal(collection["price_" + intI]);
								itemAmount = Convert.ToDecimal(collection["amount_" + intI]);
								itemImage = Convert.ToString(collection["hdnimage_" + intI]);

								// If Item Is Selected Then Only Add It To Cart
								if (itemId > 0)
								{
									this.AddCartItems(new CartItemEntityModel()
									{ });
								}
							}
						}
					}
					return Cart.Entity.cartid;
				}
			}
			catch (Exception ex)
			{
				var error = ex.InnerException;
			}

			return 0;
		}

		public long AddCustomItem(string itemCode, string itemName, decimal itemPrice, int itemQty)
		{
			var existItem = _repoSupervisor.Items.GetItemByName(itemName);
			if (existItem == null || existItem.ItemId == 0)
			{
				var item = _repoSupervisor.Items.AddNewItem(new ItemEntityModel()
				{
					Code = itemCode,
					Name = itemName,
					Description = itemName,
					PurchaseAmount = itemPrice,
					SellAmount = itemPrice,
					OpeningQuantity = itemQty
				});
				_repoSupervisor.Complete();
				return item.Entity.itemid;
			}
			return existItem.ItemId.Value;
		}

		public bool UpdateCart(CartEntityModel CartEntity, IFormCollection collection)
		{
			if (_repoSupervisor.Cart.UpdateCart(CartEntity))
			{
				_repoSupervisor.Complete();
				if (CartEntity.cartid > 0)
				{
					int.TryParse(collection["hdnTotalItems"], out int TotalItems);
					for (int intI = 1; intI <= TotalItems; intI++)
					{
						string itemAction = Convert.ToString(collection["itemAction_" + intI]);
						long.TryParse(Convert.ToString(collection["itemId_" + intI]), out long itemId);
						if (itemAction != "DA")
						{
							string itemName = Convert.ToString(collection["txtItem_" + intI]);
							int itemQty = Convert.ToInt32(collection["quantity_" + intI]);
							decimal itemPrice = Convert.ToDecimal(collection["price_" + intI]);
							decimal itemAmount = Convert.ToDecimal(collection["amount_" + intI]);
							string itemimage = Convert.ToString(collection["hdnimage_" + intI]);
							int.TryParse(Convert.ToString(collection["CartItemId_" + intI]), out int CartItemId);
							string itemCode = Convert.ToString(collection["itemCode_" + intI]);

							// If Item Is Selected Then Only Allow It To Update Else Send For Remove That Item From The List
							if (CartItemId > 0 && itemId <= 0)
							{
								itemAction = "DE";
							}

							if (itemAction == "A")
							{
								// If Item Is Selected Then Only Add It To Cart
								if (itemId > 0)
								{
									this.AddCartItems(new CartItemEntityModel()
									{
										//CartId = CartEntity.CartId,
										//ItemId = itemId,
										//Name = itemName,
										//Amount = itemAmount,
										//Quantity = itemQty,
										//SellingPrice = Convert.ToDecimal(itemPrice),
										//Image = itemimage
									});
								}
							}
							else if (itemAction == "E")
							{
								this.UpdateCartItems(new CartItemEntityModel()
								{
									//CartItemId = CartItemId,
									//CartId = CartEntity.CartId,
									//ItemId = itemId,
									//Name = itemName,
									//Amount = itemAmount,
									//Quantity = itemQty,
									//SellingPrice = Convert.ToDecimal(itemPrice),
									//Image = itemimage
								});
							}
							else if (itemAction == "DE")
							{
								this.DeleteCartItems(CartItemId);
							}
						}
					}
				}
				return true;
			}
			return false;
		}

		public bool IsCartCodeExist(string CartCode)
		{
			if (_repoSupervisor.Cart.IsCartCodeExist(CartCode))
			{
				return true;
			}
			return false;
		}

		public bool DeleteCart(long id)
		{
			_repoSupervisor.Cart.DeleteCartItems(id);
			//if (_repoSupervisor.Cart.DeleteCart(id))
			//{
			_repoSupervisor.Complete();
			return true;
			//}
			//return false;
		}

		public bool ActiveInactiveCart(long id, bool status)
		{
			if (_repoSupervisor.Cart.ActiveInactiveCart(id, status))
			{
				_repoSupervisor.Complete();
				return true;
			}
			return false;
		}

		public IEnumerable<CustomerEntityModel> GetAllCustomer()
		{
			var customers = _repoSupervisor.Customer.GetAllCustomers();
			return customers;
		}

		public CartEntityModel GetLastCart()
		{
			return _repoSupervisor.Cart.GetLastCart();
		}

		public List<ItemEntityModel> GetSerchItems(string term)
		{
			return _repoSupervisor.Cart.GetSerchItems(term);
		}

		public ObjectResult AddCartItems(CartItemEntityModel CartItemEntity)
		{
			try
			{
				var CartItems = _repoSupervisor.Cart.AddCartItems(CartItemEntity);
				if (CartItems != null)
				{
					_repoSupervisor.Complete();
					return Utils.GetObjectResult(200, new CommonAPIReponse<string>
					{
						Message = "Item added successfully.",
						Status = 200
					});
				}
			}
			catch (Exception ex)
			{
				return Utils.GetObjectResult(400, new CommonAPIReponse<string>
				{
					Message = "Item not added in cart.",
					Status = 400,
					Error = ex.Message
				});
			}
			return Utils.GetObjectResult(400, new CommonAPIReponse<string>
			{
				Message = "Item not added in cart.",
				Status = 400
			});
		}

		public ObjectResult UpdateCartItems(CartItemEntityModel CartItemEntity)
		{
			try
			{
				if (_repoSupervisor.Cart.UpdateCartItems(CartItemEntity))
				{
					_repoSupervisor.Complete();
					return Utils.GetObjectResult(200, new CommonAPIReponse<string>
					{
						Message = "Item update successfully.",
						Status = 200
					});
				}
			}
			catch (Exception ex)
			{
				return Utils.GetObjectResult(400, new CommonAPIReponse<string>
				{
					Message = "Item not updated in cart.",
					Status = 400,
					Error = ex.Message
				});
			}
			return Utils.GetObjectResult(400, new CommonAPIReponse<string>
			{
				Message = "Item not updated in cart.",
				Status = 400
			});
		}

		public ObjectResult DeleteCartItems(long id)
		{
			try
			{
				if (_repoSupervisor.Cart.DeleteCartItemsByCartItemId(id))
				{
					_repoSupervisor.Complete();
					return Utils.GetObjectResult(200, new CommonAPIReponse<string>
					{
						Message = "Item delete from cart successfully.",
						Status = 200
					});
				}
				return Utils.GetObjectResult(400, new CommonAPIReponse<string>
				{
					Message = "Item not delete from cart.",
					Status = 400
				});
			}
			catch (Exception ex)
			{
				return Utils.GetObjectResult(400, new CommonAPIReponse<string>
				{
					Message = "Item not delete from cart.",
					Status = 400,
					Error = ex.Message
				});
			}
		}

		public ObjectResult getCustomerCartDetailsById(int customerId)
		{
			return Utils.GetObjectResult(200, new CommonAPIReponse<List<CartItemEntityModel>>
			{
				Data = _repoSupervisor.Cart.getCustomerCartDetailsById(customerId),
				Status = 200
			});
		}

		public ObjectResult UpsertCart(CartItemEntityModel cartItemEntityModel)
		{
			try
			{
				var cartId = IsCartExist(cartItemEntityModel.customerloginid);
				if (cartId > 0)
				{
					cartItemEntityModel.cartid = cartId;
					var CartItems = _repoSupervisor.Cart.AddCartItems(cartItemEntityModel);
					if (CartItems != null)
					{
						_repoSupervisor.Complete();
						return Utils.GetObjectResult(200, new CommonAPIReponse<string>()
						{
							Message = "Cart update successfully.",
							Status = 200
						});
					}
				}
				else
				{
					CartEntityModel cartEntityModel = new()
					{
						customerloginid = cartItemEntityModel.customerloginid
					};
					var cart = _repoSupervisor.Cart.AddNewCart(cartEntityModel);
					_repoSupervisor.Complete();
					if (cart != null)
						cartItemEntityModel.cartid = cart.Entity.cartid;
					var CartItems = _repoSupervisor.Cart.AddCartItems(cartItemEntityModel);
					if (CartItems != null)
					{
						_repoSupervisor.Complete();
						return Utils.GetObjectResult(200, new CommonAPIReponse<string>()
						{
							Message = "Cart update successfully.",
							Status = 200
						});
					}
				}
				return Utils.GetObjectResult(400, new CommonAPIReponse<string>()
				{
					Message = "Invalid request.",
					Status = 400
				});
			}
			catch (Exception ex)
			{
				return Utils.GetObjectResult(400, new CommonAPIReponse<string>()
				{
					Message = "Invalid request.",
					Status = 400,
					Error = ex.Message
				});
			}
		}
		private int IsCartExist(int customerloginid)
		{
			return _repoSupervisor.Cart.IsCartExist(customerloginid);
		}
	}
}
