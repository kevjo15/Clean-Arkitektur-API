using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, User>
    {
        public readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserByIdCommandHandler> _logger;

        public DeleteUserByIdCommandHandler(IUserRepository userRepository, ILogger<DeleteUserByIdCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<User> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Attempting to delete user with ID: {request.Id}");

            User userToDelete = await _userRepository.GetByIdAsync(request.Id);
            if (userToDelete == null)
            {
                _logger.LogWarning($"User with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                await _userRepository.DeleteAsync(request.Id);
                _logger.LogInformation($"User with ID: {request.Id} has been successfully deleted.");
                return userToDelete;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting user with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //    User userToDelete = await _userRepository.GetByIdAsync(request.Id);

            //    if (userToDelete == null)
            //    {
            //        throw new InvalidOperationException("No user with the given ID was found.");
            //    }

            //    await _userRepository.DeleteAsync(request.Id);

            //    return (userToDelete);
            //}
        }
    }
}
