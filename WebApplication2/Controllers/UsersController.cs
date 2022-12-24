using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Models;
using WebApplication2.Queries;

namespace WebApplication2.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<ActionResult> GetUser(Guid userId)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { UserId = userId });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(CreateUserDto createUserDto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = createUserDto.Name,
            };

            var result = await _mediator.Send(new AddUserCommand { User = user });

            return Ok(result);
        }
    }   
}
