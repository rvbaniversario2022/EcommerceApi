using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;
using WebApplication2.Queries;

namespace WebApplication2.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var result = await _mediator.Send(new GetOrdersQuery());

            return Ok(result);
        }
        [HttpGet]
        [Route("{orderId:Guid}")]
        public async Task<ActionResult> GetOrder([FromRoute] Guid orderId)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery 
            { 
                OrderId = orderId 
            });

            return Ok(result);
        }

        [HttpPut]
        [Route("{orderId:Guid}")]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderDto dto, [FromRoute]Guid orderId)
        {
            var result = await _mediator.Send(new UpdateOrderCommand
            {
                OrderId = orderId,
                Status = dto.Status,
            });

            return Ok(result);
        }

        [HttpDelete]
        [Route("{orderId:Guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid orderId)
        {
            var result = await _mediator.Send(new DeleteOrderCommand
            {
                OrderId = orderId
            });

            return Ok(result);
        }
    }
}
