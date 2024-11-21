using AutoMapper;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.Constants.Exceptions;
using ECom.Data;
using ECom.Data.Account;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace ECom.BLogic.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public OrderService(IUserService userService, IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<OrderListDTO> CreateOrderListAsync(OrderListDTO orderListDTO)
        {
            EComUser user = await _userService.GetUserAsync(orderListDTO.UserClaim);
            await ConfirmUserNoAnyUnfinalizedLists(user);

            var productIds = orderListDTO.Orders.Select(p => p.ProductId).ToList();
            await ConfirmAllProductsExists(productIds);

            var orderList = new Data.Models.OrderList
            {
                Customer = user,
                LastModified = DateTime.Now
            };

            await _context.OrderLists.AddAsync(orderList);
            await _context.SaveChangesAsync();

            List<Data.Models.Order> ordersInList = await CreateOrdersForOrderList(orderList, orderListDTO.Orders);
            orderList.Orders = ordersInList;
            await _context.SaveChangesAsync();
            Log.Information("Order created.");

            return _mapper.Map<OrderListDTO>(orderList);
        }

        private async Task<List<Data.Models.Order>> CreateOrdersForOrderList(Data.Models.OrderList orderList, List<OrderDTO> orders)
        {
            List<Data.Models.Order> ordersInList = new List<Data.Models.Order>();
            foreach (var orderDTO in orders)
            {
                var order = _mapper.Map<Data.Models.Order>(orderDTO);
                order.OrderList = orderList;
                _context.Orders.Add(order);
                ordersInList.Add(order);
            }
            await _context.SaveChangesAsync();
            return ordersInList;
        }
        private async Task<Data.Models.OrderList> GetOrderList(int orderListId, EComUser user)
        {
            var orderList = await _context.OrderLists.Include(o => o.Orders)
                                                    .FirstOrDefaultAsync(o => o.Id == orderListId && o.CustomerId == user.Id);
            if (orderList is null)
            {
                var message = "Order list not found.";
                Log.Error(message);
                throw new ElementNotFoundException(message);
            }
            return orderList!;
        }

        public async Task DeleteOrderListAsync(OrderListDescriptionDTO descriptionDTO)
        {

            var user = await _userService.GetUserAsync(descriptionDTO.UserClaim);
            Data.Models.OrderList orderList = await GetOrderList(descriptionDTO.OrderListId, user);
            if (orderList.IsFinalized)
            {
                var message = "Since the order is already finalized, it cannot be removed.";
                Log.Error(message);
                throw new InvalidDeletionException(message);
            }
            _context.OrderLists.Remove(orderList);
            await _context.SaveChangesAsync();
            Log.Information("Order deleted.");
        }

        private async Task<Data.Models.OrderList?> GetLastUnfinalizedOrderList(EComUser user)
            => await _context.OrderLists.Include(o => o.Orders)
                                        .Where(o => o.CustomerId == user.Id && !o.IsFinalized)
                                        .OrderByDescending(o => o.LastModified)
                                        .FirstOrDefaultAsync();

        private async Task<Data.Models.OrderList?> GetLastModifiedOrderList(EComUser user)
            => await _context.OrderLists.Include(o => o.Orders)
                                        .Where(o => o.CustomerId == user.Id)
                                        .OrderByDescending(o => o.LastModified)
                                        .FirstOrDefaultAsync();

        public async Task<OrderListDTO> GetUserDefaultOrderList(ClaimsPrincipal userClaim)
        {
            EComUser user = await _userService.GetUserAsync(userClaim);

            Data.Models.OrderList? orderList = await GetLastUnfinalizedOrderList(user);
            orderList = orderList is null
                      ? await GetLastModifiedOrderList(user)
                      : orderList;

            if (orderList is null)
            {
                var message = "User don't have any order lists";
                Log.Error(message);
                throw new ElementNotFoundException(message);
            }

            return _mapper.Map<OrderListDTO>(orderList);

        }

        public async Task<OrderListDTO> GetOrderList(OrderListDescriptionDTO descriptionDTO)
        {
            EComUser user = await _userService.GetUserAsync(descriptionDTO.UserClaim);
            Data.Models.OrderList orderList = await GetOrderList(descriptionDTO.OrderListId, user);
            return _mapper.Map<OrderListDTO>(orderList);
        }

        private async Task ConfirmAllProductsExists(List<int> productIds)
        {
            int foundedProductsCount = await _context.Products.Where(p => productIds.Contains(p.Id)).CountAsync();
            if (foundedProductsCount != productIds.Count())
            {
                var message = "Some products are not found.";
                Log.Error(message);
                throw new InvalidArgumentException(message);
            }

        }
        private async Task ConfirmUserNoAnyUnfinalizedLists(EComUser user)
        {
            bool unfinalizedOrderExists = await _context.OrderLists.AnyAsync(o => o.CustomerId == user.Id && o.IsFinalized == false);
            if (unfinalizedOrderExists)
            {
                var message = "User already have unfinalized orders.";
                Log.Error(message);
                throw new InvalidCreationException(message);
            }
        }

        public async Task FinalizeUserOrderList(ClaimsPrincipal userClaim)
        {
            EComUser user = await _userService.GetUserAsync(userClaim);
            Data.Models.OrderList? orderList = await GetLastUnfinalizedOrderList(user);
            if (orderList is null)
            {
                throw new ElementNotFoundException("No unfinalized order lists found.");
            }
            orderList.IsFinalized = true;
            orderList.LastModified = DateTime.Now;
            _context.OrderLists.Update(orderList);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderListDTO> UpdateOrderListAsync(OrderListDTO orderListDTO)
        {
            EComUser user = await _userService.GetUserAsync(orderListDTO.UserClaim);
            Data.Models.OrderList orderList;

            try
            {
                orderList = await GetOrderList(orderListDTO.Id, user);
            }
            catch (ElementNotFoundException)
            {
                return await CreateOrderListAsync(orderListDTO);
            }

            if (orderList.IsFinalized)
            {
                var message = "Since the order is already finalized, it cannot be updated.";
                Log.Error(message);
                throw new InvalidUpdateException(message);
            }

            var productIds = orderListDTO.Orders.Select(p => p.ProductId).ToList();
            await ConfirmAllProductsExists(productIds);

            _context.Orders.RemoveRange(orderList.Orders);
            orderList.LastModified = DateTime.Now;
            orderList.Orders = await CreateOrdersForOrderList(orderList, orderListDTO.Orders);
            _context.OrderLists.Update(orderList);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderListDTO>(orderList);
        }
    }
}
