using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;
using WebApplication2.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication2.Controllers
{
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/cart-items")]
    [ApiController]
    public class CartItemsV2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemsV2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetCartItems()
        {
            var result = await _mediator.Send(new GetCartItemsQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddCartItem([FromBody] AddCartItemCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Route("{itemId:Guid}")]
        public async Task<ActionResult> UpdateCartItem([FromBody] UpdateCartItemDto dto, [FromRoute] Guid itemId)
        {
            var result = await _mediator.Send(new UpdateCartItemCommand
            {
                Id = itemId,
                ProductName = dto.ProductName,
            });

            return Ok(result);
        }

        [HttpDelete]
        [Route("{itemId:Guid}")]
        public async Task<ActionResult> DeleteCartItem([FromRoute] Guid itemId)
        {
            var result = await _mediator.Send(new DeleteCartItemCommand
            {
                ItemId = itemId
            });

            return Ok(result);
        }
    }
}
