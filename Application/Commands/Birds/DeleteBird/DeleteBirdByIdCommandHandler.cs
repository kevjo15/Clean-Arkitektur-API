using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<DeleteBirdByIdCommandHandler> _logger;

        public DeleteBirdByIdCommandHandler(IBirdRepository birdRepository, ILogger<DeleteBirdByIdCommandHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }

        public async Task<Bird> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to delete bird with ID: {request.Id}");

            var birdToDelete = await _birdRepository.GetByIdAsync(request.Id);
            if (birdToDelete == null)
            {
                _logger.LogWarning($"Bird with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                await _birdRepository.DeleteAsync(request.Id);
                _logger.LogInformation($"Bird with ID: {request.Id} has been successfully deleted.");
                return birdToDelete;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting bird with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //var birdToDelete = await _birdRepository.GetByIdAsync(request.Id);
            //if (birdToDelete == null)
            //{
            //    throw new InvalidOperationException("No cat with the given ID was found.");
            //}

            //await _birdRepository.DeleteAsync(request.Id);

            //return birdToDelete;
        }
    }
}
