using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.Username,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password), 
                
            };

            await _userRepository.AddAsync(userToCreate);

            return userToCreate;
        }
    }
}
