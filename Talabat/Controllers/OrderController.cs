using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.DTOs;
using Talabat.Helper;
using TalabatCore.Entities.Identity;
using TalabatCore.Entities.OrderAggregate;
using TalabatCore.OrderService;

namespace Talabat.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orderaddress=mapper.Map<AddressDto,TalabatCore.Entities.OrderAggregate.Address>(orderDto.ShippingAddress);

            var order =await orderService.CreateOrderAsync(email, orderDto.BasketId, orderDto.DeliveryMethodId, orderaddress);
            var orderdto = mapper.Map<OrderToReturnDto>(order);
            if (orderdto == null) { return BadRequest(new ApiResponse(400)); }
            return Ok(orderdto);
            
        }
        [Authorize]
        [HttpGet("GetOrder")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var user = User.FindFirstValue(ClaimTypes.Email);
            var order =await orderService.GetOrderByuserAsync(user);
            var orederDto = mapper.Map<OrderToReturnDto>(order);
            return Ok(orederDto);
        }
        [Authorize]
        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var DeliveryMethods= await orderService.GetDeliveryMethodAsync();
            return Ok(DeliveryMethods);
        }
    }
}
