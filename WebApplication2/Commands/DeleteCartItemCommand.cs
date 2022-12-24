using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class DeleteCartItemCommand : IRequest<CartItem>
    {
         public Guid ItemId { get; set; }
    }
}
