using MediatR;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepo;

        public GetUserByIdHandler(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return await _userRepo.GetUserById(query.UserId);
        }
    }
}
