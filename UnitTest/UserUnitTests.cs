using FluentAssertions;
using Moq;
using UnitTest.Mocks;
using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Handlers;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;

namespace UnitTest
{
    public class UserUnitTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly List<User> _userList;

        public UserUnitTests()
        {
            _mockRepo = MockUserRepository.GetUserRepo();
            _userList = MockUserRepository.UserList;
        }

        [Fact]
        public async Task GetUserByIdTest()
        {
            var userId = new Guid("0A82109D-A736-41BB-8A8A-2F94AF66AD50");

            var handler = new GetUserByIdHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetUserByIdQuery { UserId = userId }, CancellationToken.None);

            result.Should().BeOfType<User>();
            result.Id.Should().Be(userId);
        }

        [Fact]
        public async Task AddUser()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "peter"
            };

            var handler = new AddUserHandler(_mockRepo.Object);
            var result = await handler.Handle(new AddUserCommand { User = user }, CancellationToken.None);

            result.Should().BeOfType<User>();
        }
    }
}
