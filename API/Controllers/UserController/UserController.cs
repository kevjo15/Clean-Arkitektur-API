//using Application.Dtos;
//using Domain.Models.User;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers.UserController
//{
//    public class UserController : Controller
//    {
//        public static User user = new User();

//        private readonly IMediator _mediator;
//        public UserController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        public async Task<IActionResult> Register([FromBody] UserDto request)
//        {
//            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
//            user.UserName = request.UserName;
//            user.UserPasswordHash = passwordHash;
//            return Ok(user);
//        }
//    }
//}