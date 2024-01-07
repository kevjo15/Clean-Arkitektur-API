using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IUserRepository userRepository, ILogger<AddUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adding a new user with username: {request.NewUser.Username}");

            User userToCreate = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.Username,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password),
            };

            try
            {
                await _userRepository.AddAsync(userToCreate);
                _logger.LogInformation($"New user added with ID: {userToCreate.Id}");
                return userToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new user");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //User userToCreate = new()
            //{
            //    Id = Guid.NewGuid(),
            //    UserName = request.NewUser.Username,
            //    UserPassword = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password), 

            //};

            //await _userRepository.AddAsync(userToCreate);

            //return userToCreate;
        }
    }
}
