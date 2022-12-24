using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var query = "SELECT * FROM Users WHERE Id = @Id";

                var connection = _context.Database.GetDbConnection();

                _logger.LogInformation($"Fetching User...");
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });

                return user;
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(GetUserById)} Could Not Fetch User With Id: {id}");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    var ex = new ArgumentNullException($"{nameof(AddUser)} user must not be null");

                    _logger.LogInformation($"Error: {ex}");

                    throw ex;
                }

                _logger.LogInformation("Adding User...");
                await _context.Users.AddAsync(user);

                _logger.LogInformation("Saving Changes...");
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(user)} could not be saved");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }
    }
}
