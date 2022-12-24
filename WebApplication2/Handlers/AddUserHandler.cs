using MediatR;
using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepo;

        public AddUserHandler(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<User> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            return await _userRepo.AddUser(command.User);
        }
    }
}
