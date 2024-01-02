using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IUserRepository userRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all users from the database");

            List<User> allUsersDatabase = await _userRepository.GetAllUsersAsync();

            if (allUsersDatabase == null || allUsersDatabase.Count == 0)
            {
                _logger.LogWarning("No users found in the database.");
                return new List<User>(); // Returnera en tom lista istället för att kasta undantag
            }

            _logger.LogInformation($"Retrieved {allUsersDatabase.Count} users from the database.");
            return allUsersDatabase;

            //List<User> allUsersDatabase = await _userRepository.GetAllUsersAsync();
            //if (allUsersDatabase == null)
            //{
            //    throw new InvalidOperationException("No Dogs was found!");
            //}

            //return allUsersDatabase;
        }
    }
}
