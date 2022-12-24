using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication2.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/cart-items")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemsController(IMediator mediator)
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
        public async Task<ActionResult> AddCartItem([FromBody] CreateCartItemDto dto)
        {
            var result = await _mediator.Send(new AddCartItemCommand 
            { 
                ProductName = dto.ProductName,
                UserId = dto.UserId,
            });

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