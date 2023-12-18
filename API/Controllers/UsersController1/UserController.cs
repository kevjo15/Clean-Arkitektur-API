using Application.Commands.Users.RegisterUser;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers.UsersController1
{
    public class UserController : Controller
    {
        internal readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] UserDto userToRegister)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand(userToRegister)));
        }
    }
}
