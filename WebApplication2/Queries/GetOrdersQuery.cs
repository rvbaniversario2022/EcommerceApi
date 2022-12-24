using Dapper;
using MediatR;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Queries
{
    public record GetOrdersQuery : IRequest<IEnumerable<Order>>;
}
