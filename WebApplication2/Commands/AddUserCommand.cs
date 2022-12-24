using MediatR;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public record AddUserCommand : IRequest<User>
    {
        public User User { get; set; }
    }
}
