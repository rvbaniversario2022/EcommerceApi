using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(Guid id);
        public Task<User> AddUser(User user);
    }
}
