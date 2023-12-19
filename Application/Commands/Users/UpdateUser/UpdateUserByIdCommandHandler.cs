using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
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

        public UpdateUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UpdateUserByIdCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
            {
                throw new InvalidOperationException($"Användare med ID {command.UserId} hittades inte.");
            }

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
            return user;
        }

    }
}
