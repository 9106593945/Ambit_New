using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Ambit.Domain.Common;
using Ambit.Domain.Entities;
using Ambit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Ambit.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepoSupervisor _repoSupervisor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;
        public OrderService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _repoSupervisor = repoSupervisor;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
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

        //public ObjectResult GetCustomerCartDetailsById()
        //{
        //    return Utils.GetObjectResult(200, new CommonAPIReponse<List<CartItemEntityModel>>
        //    {
        //        Data = _repoSupervisor.Cart.GetCartDetailsByCustomerLoginId(customerId),
        //        Status = 200
        //    });
        //}

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

        public ObjectResult GetOrderDetailsByCustomerLoginId()
        {
            var currentUserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            int customerId = 0;
            if (!string.IsNullOrWhiteSpace(currentUserName))
            {
                var loginusr = _dbContext.CustomerLogin.First(x => x.Name == currentUserName);
                if(loginusr != null)
                { 
                    customerId = Convert.ToInt32(loginusr.Customerid);
                }
            }
            var data = _repoSupervisor.Order.GetOrderDetailsByCustomerLoginId(Convert.ToInt32(customerId));
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    if (item.OrderItems == null)
                    {
                        item.OrderItems = new List<OrderItemEntityModel>();
                    }
                    item.OrderItems.AddRange(_repoSupervisor.Order.GetOrderItemsById(Convert.ToInt32(item.orderid)));
                }
            }
            return Utils.GetObjectResult(200, new CommonAPIReponse<List<OrderEntityModel>>
            {
                Data = data,
                Status = 200
            });
        }

        public ObjectResult AddOrder(OrderEntityModel orderEntityModel)
        {
            try
            {
                var currentUserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                int customerId = 0;
                if (!string.IsNullOrWhiteSpace(currentUserName))
                {
                    var loginusr = _dbContext.CustomerLogin.First(x => x.Name == currentUserName);
                    if (loginusr != null)
                    {
                        customerId = Convert.ToInt32(loginusr.Customerid);
                    }
                }
                orderEntityModel.CustomerId = customerId;
                var order = _repoSupervisor.Order.AddOrder(orderEntityModel);
                _repoSupervisor.Complete();
                if (order != null)
                {
                    foreach (var item in orderEntityModel.OrderItems)
                    {
                        item.OrderId = Convert.ToInt32(order.Entity.orderid);
                        item.CustomerId = customerId;
                        item.customername = order.Entity.customername;
                        var OrderItems = _repoSupervisor.Order.AddOrderItems(item);
                        if (OrderItems == null)
                        {
                            _repoSupervisor.Complete();
                            return Utils.GetObjectResult(400, new CommonAPIReponse<string>()
                            {
                                Message = "Order not Added successfully.",
                                Status = 400
                            });
                        }
                    }
                    _repoSupervisor.Complete();
                    return Utils.GetObjectResult(200, new CommonAPIReponse<string>()
                    {
                        Message = "Order Added successfully.",
                        Status = 200
                    });
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

        public ObjectResult GetByOrderId(int orderId)
        {
            var data = _repoSupervisor.Order.GetByOrderId(orderId);
            if(data != null)
            {
                if(data.OrderItems == null)
                {
                    data.OrderItems = new List<OrderItemEntityModel>();
                }
                data.OrderItems.AddRange(_repoSupervisor.Order.GetOrderItemsById(orderId));
            }
            return Utils.GetObjectResult(200, new CommonAPIReponse<OrderEntityModel>
            {
                Data = data,
                Status = 200
            });
        }
    }
}
