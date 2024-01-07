using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<UpdateBirdByIdCommandHandler> _logger;

        public UpdateBirdByIdCommandHandler(IBirdRepository birdRepository, ILogger<UpdateBirdByIdCommandHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }
        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update bird with ID: {request.Id}");

            Bird birdToUpdate = await _birdRepository.GetByIdAsync(request.Id);
            if (birdToUpdate == null)
            {
                _logger.LogWarning($"Bird with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.CanFly = request.UpdatedBird.CanFly;
                birdToUpdate.Color = request.UpdatedBird.Color;

                await _birdRepository.UpdateAsync(birdToUpdate);
                _logger.LogInformation($"Bird with ID: {request.Id} has been successfully updated.");

                return birdToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating bird with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //Bird birdToUpdate = await _birdRepository.GetByIdAsync(request.Id);
            //if (birdToUpdate == null)
            //{
            //    return null!;
            //}

            //birdToUpdate.Name = request.UpdatedBird.Name;
            //birdToUpdate.CanFly = request.UpdatedBird.CanFly;
            //birdToUpdate.Color = request.UpdatedBird.Color;
            //await _birdRepository.UpdateAsync(birdToUpdate);

            //return birdToUpdate;

        }
    }
}
