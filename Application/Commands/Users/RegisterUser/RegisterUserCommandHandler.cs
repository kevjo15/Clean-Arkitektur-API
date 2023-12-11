//using Domain.Models.User;
//using Infrastructure.Database.Repositories.Users;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Models;
//using Infrastructure.Database.Repository;

//namespace Application.Commands.Users.RegisterUser
//{
//    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
//    {
//        private readonly IUserRepository _userRepository;
//        public RegisterUserCommandHandler(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
//        {

//            var userToCreate = new User
//            {
//                UserId = Guid.NewGuid(),
//                UserName = request.NewUser.UserName,
//                UserPasswordHash = request.NewUser.Password,
//            };

//            var createdUser = await _userRepository.RegisterUser(userToCreate);

//            return createdUser;
//        }
//    }
//}
