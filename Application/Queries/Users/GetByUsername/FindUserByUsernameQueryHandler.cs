using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetByUsername
{
    public class FindUserByUsernameQueryHandler : IRequestHandler<FindUserByUsernameQuery, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<FindUserByUsernameQueryHandler> _logger;

        public FindUserByUsernameQueryHandler(IUserRepository userRepository, ILogger<FindUserByUsernameQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<User> Handle(FindUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                _logger.LogError("Username cannot be null or empty.");
                throw new ArgumentException("Username cannot be null or empty.", nameof(request.Username));
            }

            var user = await _userRepository.FindByUsernameAsync(request.Username);
            if (user == null)
            {
                _logger.LogWarning($"User with username '{request.Username}' was not found.");
                throw new KeyNotFoundException($"Användare med användarnamnet '{request.Username}' hittades inte.");
            }

            return user;
        }
    }
}
