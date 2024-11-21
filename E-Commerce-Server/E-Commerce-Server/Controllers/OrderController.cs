using AutoMapper;
using ECom.API.Exchanges.Order;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrderList(int? orderListId)
        {
            OrderListDTO result;
            if (orderListId is null)
            {
                result = await _orderService.GetUserDefaultOrderList(HttpContext.User);
            }
            else
            {
                OrderListDescriptionDTO descriptionDTO = new OrderListDescriptionDTO
                {
                    OrderListId = (int)orderListId,
                    UserClaim = HttpContext.User
                };
                result = await _orderService.GetOrderList(descriptionDTO);
            }

            return Ok(_mapper.Map<OrderListExchange>(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderListRequest request)
        {
            var orderListDTO = _mapper.Map<OrderListDTO>(request);
            orderListDTO.UserClaim = HttpContext.User;
            var result = await _orderService.CreateOrderListAsync(orderListDTO);
            return Ok(_mapper.Map<OrderListExchange>(result));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderListExchange orderList)
        {
            var orderListDTO = _mapper.Map<OrderListDTO>(orderList);
            orderListDTO.UserClaim = HttpContext.User;
            var resultDTO = await _orderService.UpdateOrderListAsync(orderListDTO);

            return Ok(_mapper.Map<OrderListExchange>(resultDTO));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderList(int orderListId)
        {
            if (orderListId < 0)
            {
                return BadRequest("Id must be non-negative");
            }
            OrderListDescriptionDTO descriptionDTO = new OrderListDescriptionDTO
            {
                OrderListId = orderListId,
                UserClaim = HttpContext.User
            };
            await _orderService.DeleteOrderListAsync(descriptionDTO);
            return NoContent();
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyProduct()
        {
            await _orderService.FinalizeUserOrderList(HttpContext.User);
            return NoContent();
        }
    }
}
