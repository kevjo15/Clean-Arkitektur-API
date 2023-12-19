using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            User userToUpdate = await _userRepository.GetByIdAsync(request.UserId);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Perform the mapping here
            if (!string.IsNullOrWhiteSpace(request.UpdateUserDto.Username))
            {
                userToUpdate.UserName = request.UpdateUserDto.Username;
            }
            if (!string.IsNullOrWhiteSpace(request.UpdateUserDto.Password))
            {
                userToUpdate.UserPassword = BCrypt.Net.BCrypt.HashPassword(request.UpdateUserDto.Password);
            }
            userToUpdate.UserPassword = request.UpdateUserDto.Username;
            userToUpdate.UserPassword = request.UpdateUserDto.Password;
            await _userRepository.UpdateAsync(userToUpdate);
            return userToUpdate;
        }

    }
}
