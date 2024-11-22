using AutoFixture;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Order;
using System.Security.Claims;

namespace ECom.Test.BLogicTests
{
    public class OrderServiceTests : ServiceWithContextTests
    {
        private IOrderService _orderService;

        public OrderServiceTests() : base("Database_Orders")
        {
            _orderService = new OrderService(_userService, _mapper, _context);
        }

        private OrderListDTO PrepareTestOrderListDTO()
        {
            var fixture = new Fixture();
            var orderListDTO = new OrderListDTO();
            orderListDTO.Orders = new List<OrderDTO>();
            orderListDTO.UserClaim = fixture.Create<ClaimsPrincipal>();
            OrderDTO orderDTO = new OrderDTO { ProductId = _context.Products.First().Id, Quantity = 1 };
            orderListDTO.Orders.Add(orderDTO);
            return orderListDTO;
        }

        private async Task EnsureTestOrderListExists()
        {
            var orderListDTO = new OrderListDTO();
            var fixture = new Fixture();
            orderListDTO = _mapper.Map<OrderListDTO>(_context.OrderLists.FirstOrDefault(r => r.CustomerId == testUser.Id));
            if (orderListDTO is null)
            {
                await _orderService.CreateOrderListAsync(PrepareTestOrderListDTO());
            }
        }

        [Fact]
        public async Task CreateOrderListAsync_ShouldCreateOrderListAndOrders_True()
        {
            //Arrange
            var orderList = _context.OrderLists.FirstOrDefault(x => x.CustomerId == testUser.Id);
            if (orderList is not null)
            { _context.OrderLists.Remove(orderList); }
            OrderListDTO orderListDTO = PrepareTestOrderListDTO();
            //Act
            var result = await _orderService.CreateOrderListAsync(orderListDTO);
            //Assert
            Assert.Equal(orderListDTO.Orders.Count, result.Orders.Count);
            Assert.Equal(orderListDTO.Orders[0].ProductId, result.Orders[0].ProductId);
            Assert.NotNull(_context.OrderLists.FirstOrDefault(r => r.Id == result.Id));
        }

        [Fact]
        public async Task DeleteOrderList_DeletesUnfinalizedOrderList_True()
        {
            //Arrange
            var orderListDTO = new OrderListDTO();
            var fixture = new Fixture();
            orderListDTO = _mapper.Map<OrderListDTO>(_context.OrderLists.FirstOrDefault(r => r.CustomerId == testUser.Id));
            if (orderListDTO is null)
            {
                orderListDTO = await _orderService.CreateOrderListAsync(PrepareTestOrderListDTO());
            }
            //Act
            var descriptionDTO = new OrderListDescriptionDTO
            {
                OrderListId = orderListDTO.Id,
                UserClaim = orderListDTO.UserClaim!
            };
            await _orderService.DeleteOrderListAsync(descriptionDTO);
            //Assert
            Assert.Null(_context.OrderLists.FirstOrDefault(r => r.Id == orderListDTO.Id));
        }

        [Fact]
        public async Task FinalizeOrderListAsync_ShouldFinalizeOrderList_True()
        {
            //Arrange
            await EnsureTestOrderListExists();
            var orderListDTO = _mapper.Map<OrderListDTO>(_context.OrderLists.FirstOrDefault(r => r.CustomerId == testUser.Id));
            //Act
            await _orderService.FinalizeUserOrderList(orderListDTO.UserClaim!);
            //Assert
            var result = _context.OrderLists.FirstOrDefault(r => r.Id == orderListDTO.Id);
            Assert.NotNull(result);
            Assert.True(result.IsFinalized);
        }



        [Fact]
        public async Task GetOrderListAsync_ShouldReturnOrderList_True()
        {
            //Arrange
            await EnsureTestOrderListExists();
            var orderListDTO = _mapper.Map<OrderListDTO>(_context.OrderLists.FirstOrDefault(r => r.CustomerId == testUser.Id));
            var descriptionDTO = new OrderListDescriptionDTO
            {
                OrderListId = orderListDTO.Id,
                UserClaim = orderListDTO.UserClaim!
            };

            //Act
            var result = await _orderService.GetOrderList(descriptionDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(orderListDTO.Orders.Count, result.Orders.Count);
            Assert.Equal(orderListDTO.Orders[0].ProductId, result.Orders[0].ProductId);
        }

        [Fact]
        public async Task UpdateOrderListAsync_ShouldReturnOrderList_True()
        {
            //Arrange
            await EnsureTestOrderListExists();
            var orderListDTO = _mapper.Map<OrderListDTO>(_context.OrderLists.FirstOrDefault(r => r.CustomerId == testUser.Id));
            uint updatedQuintity = 2;
            orderListDTO.Orders[0].Quantity = updatedQuintity;

            //Act
            var result = await _orderService.UpdateOrderListAsync(orderListDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(orderListDTO.Orders.Count, result.Orders.Count);
            Assert.Equal(orderListDTO.Orders[0].ProductId, result.Orders[0].ProductId);
            Assert.Equal(updatedQuintity, result.Orders[0].Quantity);
        }
    }
}
