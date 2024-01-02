using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<AddBirdCommandHandler> _logger;

        public AddBirdCommandHandler(IBirdRepository birdRepository, ILogger<AddBirdCommandHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }
        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a new bird");

            Bird birdToCreate = new Bird
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name,
                Color = request.NewBird.Color,
                CanFly = request.NewBird.CanFly,
            };

            try
            {
                await _birdRepository.AddAsync(birdToCreate);
                _logger.LogInformation($"New bird added with ID: {birdToCreate.Id}");
                return birdToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new bird");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }
            //Bird BirdToCreate = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = request.NewBird.Name,
            //    Color = request.NewBird.Color,
            //    CanFly = request.NewBird.CanFly,
            //};
            //_birdRepository.AddAsync(BirdToCreate);

            //return Task.FromResult(BirdToCreate);
        }
    }
}
