using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving user with ID: {request.Id}");

            User wantedUser = await _userRepository.GetByIdAsync(request.Id);

            if (wantedUser == null)
            {
                _logger.LogWarning($"User with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            return wantedUser;

            //User wantedUser = await _userRepository.GetByIdAsync(request.Id);
            //try
            //{
            //    if (wantedUser == null)
            //    {
            //        return null!;

            //    }
            //    return wantedUser;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

        }
    }
}
