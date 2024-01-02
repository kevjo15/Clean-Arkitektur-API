using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;
        private readonly ILogger<AddDogCommandHandler> _logger;

        public AddDogCommandHandler(IDogRepository dogRepository, ILogger<AddDogCommandHandler> logger)
        {
            _dogRepository = dogRepository;
            _logger = logger;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a new dog");

            Dog dogToCreate = new Dog
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                Breed = request.NewDog.Breed,
                Weight = request.NewDog.Weight
            };

            try
            {
                await _dogRepository.AddAsync(dogToCreate);
                _logger.LogInformation($"New dog added with ID: {dogToCreate.Id}");
                return dogToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new dog");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //Dog dogToCreate = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = request.NewDog.Name,
            //    Breed = request.NewDog.Breed,
            //    Weight = request.NewDog.Weight
            //};

            //await _dogRepository.AddAsync(dogToCreate);

            //return dogToCreate;
        }
    }
}
