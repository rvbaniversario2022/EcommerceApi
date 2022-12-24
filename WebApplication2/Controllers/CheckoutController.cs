using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public CheckoutController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
        {
            var order = _context.Orders.Where(x => x.CartItems.Any(x => x.UserId == checkoutDto.UserId) && x.Status == Status.Pending).FirstOrDefault();

            if (order == null)
            {
                return BadRequest($"No Pending Order Found For User Id: {checkoutDto.UserId}");
            }

            return Ok(await _mediator.Send(new CheckoutCommand { Order = order}));
        }
    }
}
