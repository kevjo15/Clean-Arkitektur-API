using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserByIdCommandHandler> _logger;

        public UpdateUserByIdCommandHandler(IUserRepository userRepository, ILogger<UpdateUserByIdCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> Handle(UpdateUserByIdCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update user with ID: {command.UserId}");

            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID: {command.UserId} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                // Uppdatera lösenordet om det är nytt
                if (!string.IsNullOrWhiteSpace(command.NewPassword))
                {
                    user.UserPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
                }

                // Uppdatera userName om det är nytt
                if (!string.IsNullOrWhiteSpace(command.UpdateUserDto.Username))
                {
                    user.UserName = command.UpdateUserDto.Username;
                }

                await _userRepository.UpdateAsync(user);
                _logger.LogInformation($"User with ID: {command.UserId} has been successfully updated.");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating user with ID: {command.UserId}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }


            //var user = await _userRepository.GetByIdAsync(command.UserId);
            //if (user == null)
            //{
            //    throw new InvalidOperationException($"Användare med ID {command.UserId} hittades inte.");
            //}

            //// Uppdatera lösenordet om det är nytt
            //if (!string.IsNullOrWhiteSpace(command.NewPassword))
            //{
            //    user.UserPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
            //}

            //// Uppdatera userName om det är nytt
            //if (!string.IsNullOrWhiteSpace(command.UpdateUserDto.Username))
            //{
            //    user.UserName = command.UpdateUserDto.Username;
            //}

            //await _userRepository.UpdateAsync(user);
            //return user;
        }

    }
}
