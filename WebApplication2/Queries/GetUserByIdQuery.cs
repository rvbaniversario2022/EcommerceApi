using Dapper;
using MediatR;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Queries
{
    public record GetUserByIdQuery: IRequest<User>
    {
        public Guid UserId { get; set; }
    }
}
