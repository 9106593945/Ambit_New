using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Ambit.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Ambit.Services
{
	public class CartService : ICartService
	{
		private readonly IRepoSupervisor _repoSupervisor;
		private readonly HttpContext _context;

		public CartService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor,
						IHttpContextAccessor httpContextAccessor)
		{
			_context = httpContextAccessor.HttpContext;
			_repoSupervisor = repoSupervisor;
		}

		public bool DeleteCart(long id)
		{
			_repoSupervisor.Cart.DeleteCartItems(id);
			_repoSupervisor.Complete();
			return true;
		}

		public ObjectResult AddCartItems(CartItemRequest request)
		{
			try
			{
				var CartItems = _repoSupervisor.Cart.AddCartItems(new CartItemEntityModel
				{
					Active = true,
					CartId = request.CartId,
					CustomerLoginId = request.CustomerLoginId,
					ItemId = request.ItemId,
					Quantity = request.Quantity,
					Created_By = 1,
					Created_On = DateTime.UtcNow,
					Id = request.Id,
					IsDeleted = false,
					Updated_By = 1,
					Updated_On = DateTime.UtcNow
				});
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
			int customerId = Convert.ToInt32(_context.User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);

			return Utils.GetObjectResult(200, new CommonAPIReponse<List<CartItemEntityModel>>
			{
				Data = _repoSupervisor.Cart.GetCartDetailsByCustomerLoginId(customerId),
				Status = 200
			});
		}

		public ObjectResult UpsertCart(CartItemRequest request)
		{
			try
			{
				var cartId = IsCartExist(request.CustomerLoginId);
				if (cartId > 0)
				{
					request.CartId = cartId;
					var CartItems = _repoSupervisor.Cart.AddCartItems(new CartItemEntityModel
					{
						Active = true,
						CartId = request.CartId,
						CustomerLoginId = request.CustomerLoginId,
						CustomerId = 1,
						ItemId = request.ItemId,
						Quantity = request.Quantity,
						Created_By = 1,
						Created_On = DateTime.UtcNow,
						Id = request.Id,
						IsDeleted = false,
						Updated_By = 1,
						Updated_On = DateTime.UtcNow
					});
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
						customerloginid = request.CustomerLoginId
					};
					var cart = _repoSupervisor.Cart.AddNewCart(cartEntityModel);
					_repoSupervisor.Complete();
					if (cart != null)
						request.CartId = cart.Entity.cartid;
					var CartItems = _repoSupervisor.Cart.AddCartItems(new CartItemEntityModel
					{
						Active = true,
						CartId = request.CartId,
						CustomerLoginId = request.CustomerLoginId,
						ItemId = request.ItemId,
						CustomerId = 1,
						Quantity = request.Quantity,
						Created_By = 1,
						Created_On = DateTime.UtcNow,
						Id = request.Id,
						IsDeleted = false,
						Updated_By = 1,
						Updated_On = DateTime.UtcNow
					});
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
		private long IsCartExist(int customerloginid)
		{
			return _repoSupervisor.Cart.IsCartExist(customerloginid);
		}
	}
}
