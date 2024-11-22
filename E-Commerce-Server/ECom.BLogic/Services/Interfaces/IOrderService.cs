using ECom.BLogic.DTOs;
using System.Security.Claims;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderListDTO> CreateOrderListAsync(OrderListDTO createOrderDTO);
        public Task DeleteOrderListAsync(OrderListDescriptionDTO descriptionDTO);
        public Task<OrderListDTO> GetUserDefaultOrderList(ClaimsPrincipal userClaim);
        public Task<OrderListDTO> GetOrderList(OrderListDescriptionDTO descriptionDTO);
        public Task FinalizeUserOrderList(ClaimsPrincipal userClaim);
        public Task<OrderListDTO> UpdateOrderListAsync(OrderListDTO orderListDTO);
    }
}
