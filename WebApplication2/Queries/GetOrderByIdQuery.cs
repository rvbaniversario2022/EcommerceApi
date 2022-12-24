using Dapper;
using MediatR;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Queries
{
    public record GetOrderByIdQuery : IRequest<Order>
    {
        public Guid OrderId { get; set; }
    }
}
