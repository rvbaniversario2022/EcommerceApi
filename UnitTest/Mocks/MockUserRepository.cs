using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace UnitTest.Mocks
{
    public static class MockUserRepository
    {
        public static List<User> UserList = (List<User>)Generate();
        public static IEnumerable<User> Generate()
        {
            Faker<User> UserGenerator = new Faker<User>()
                .RuleFor(item => item.Id, new Guid("0A82109D-A736-41BB-8A8A-2F94AF66AD50"))
                .RuleFor(user => user.Name, bogus => bogus.Name.FirstName());

            return UserGenerator.Generate(1);
        }

        public static Mock<IUserRepository> GetUserRepo()
        {
            var users = new List<User>(Generate());

            var mockRepo = new Mock<IUserRepository>();

            mockRepo.Setup(r => r.GetUserById(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                var user = users.FirstOrDefault(x => x.Id == id);

                return user;
            });

            mockRepo.Setup(r => r.AddUser(It.IsAny<User>())).ReturnsAsync((User user) =>
            {
                UserList.Add(user);

                return user;
            });

            return mockRepo;
        }
    }
}
