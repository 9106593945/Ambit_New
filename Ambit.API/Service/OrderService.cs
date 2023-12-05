using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ambit.Services
{
	public class OrderService : IOrderService
	{
		private readonly AppSettings _appSettings;
		private readonly IRepoSupervisor _repoSupervisor;
		public OrderService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
		{
			_appSettings = appSettings.Value;
			_repoSupervisor = repoSupervisor;
		}

		public bool DeleteCart(long id)
		{
			_repoSupervisor.Cart.DeleteCartItems(id);
			_repoSupervisor.Complete();
			return true;
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
				if (_repoSupervisor.Cart.DeleteCartItems(id))
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

		public ObjectResult GetCustomerCartDetailsById()
		{
			return Utils.GetObjectResult(200, new CommonAPIReponse<List<CartItemEntityModel>>
			{
				Data = _repoSupervisor.Cart.GetCartDetailsByCustomerLoginId(customerId),
				Status = 200
			});
		}

		public ObjectResult UpsertCart(CartItemEntityModel cartItemEntityModel)
		{
			try
			{
				var cartId = IsCartExist(cartItemEntityModel.CustomerLoginId);
				if (cartId > 0)
				{
					cartItemEntityModel.CartId = cartId;
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
						customerloginid = cartItemEntityModel.CustomerLoginId
					};
					var cart = _repoSupervisor.Cart.AddNewCart(cartEntityModel);
					_repoSupervisor.Complete();
					if (cart != null)
						cartItemEntityModel.CartId = cart.Entity.cartid;
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
