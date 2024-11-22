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

        /// <summary>
        /// Get user order list by id or default
        /// </summary>
        /// <remarks>Default means unfinalized orderList or last modified orderList </remarks>
        /// <param name="orderListId"></param>
        /// <response code="200">Returns found order list</response>
        /// <response code="400">If id is negative</response>
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
                if (orderListId < 0)
                {
                    return BadRequest("Id must be non-negative");
                }

                OrderListDescriptionDTO descriptionDTO = new OrderListDescriptionDTO
                {
                    OrderListId = (int)orderListId,
                    UserClaim = HttpContext.User
                };
                result = await _orderService.GetOrderList(descriptionDTO);
            }

            return Ok(_mapper.Map<OrderListExchange>(result));
        }

        /// <summary>
        /// Creates unfinalized order list
        /// </summary>
        /// <remarks> A user cannot create a new order list if they have an unfinalized one. </remarks>
        /// <param name="request"></param>
        /// <response code="200">Returns created order list</response>
        [HttpPost]
        public async Task<IActionResult> CreateOrderList(CreateOrderListRequest request)
        {
            var orderListDTO = _mapper.Map<OrderListDTO>(request);
            orderListDTO.UserClaim = HttpContext.User;
            var result = await _orderService.CreateOrderListAsync(orderListDTO);
            return Ok(_mapper.Map<OrderListExchange>(result));
        }

        /// <summary>
        /// Updates order list
        /// </summary>
        /// <remarks> Only unfinalized order lists can be updated</remarks>
        /// <param name="orderList"></param>
        /// <response code="200">Returns updated order list</response>
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderListExchange orderList)
        {
            var orderListDTO = _mapper.Map<OrderListDTO>(orderList);
            orderListDTO.UserClaim = HttpContext.User;
            var resultDTO = await _orderService.UpdateOrderListAsync(orderListDTO);

            return Ok(_mapper.Map<OrderListExchange>(resultDTO));
        }

        /// <summary>
        /// Deletes order list
        /// </summary>
        /// <param name="orderListId"></param>
        /// <response code="204"></response>
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

        /// <summary>
        /// Finalize the user order list
        /// </summary>
        /// <response code="204"></response>
        [HttpPost("buy")]
        public async Task<IActionResult> BuyProduct()
        {
            await _orderService.FinalizeUserOrderList(HttpContext.User);
            return NoContent();
        }
    }
}
