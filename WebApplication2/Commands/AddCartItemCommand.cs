using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class AddCartItemCommand : IRequest<CartItem>
    {
        public string? ProductName { get; set; }
        public Guid UserId { get; set; }
    }
}
