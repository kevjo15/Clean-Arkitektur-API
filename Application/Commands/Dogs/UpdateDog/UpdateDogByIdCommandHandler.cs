using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;
        private readonly ILogger<UpdateDogByIdCommandHandler> _logger;

        public UpdateDogByIdCommandHandler(IDogRepository dogRepository, ILogger<UpdateDogByIdCommandHandler> logger)
        {
            _dogRepository = dogRepository;
            _logger = logger;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update dog with ID: {request.Id}");

            Dog dogToUpdate = await _dogRepository.GetByIdAsync(request.Id);
            if (dogToUpdate == null)
            {
                _logger.LogWarning($"Dog with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                dogToUpdate.Name = request.UpdatedDog.Name;
                dogToUpdate.Breed = request.UpdatedDog.Breed;
                dogToUpdate.Weight = request.UpdatedDog.Weight;
                // Uppdatera eventuella andra fält från request.UpdatedDog här

                await _dogRepository.UpdateAsync(dogToUpdate);
                _logger.LogInformation($"Dog with ID: {request.Id} has been successfully updated.");

                return dogToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating dog with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //Dog dogToUpdate = await _dogRepository.GetByIdAsync(request.Id);

            //if (dogToUpdate == null)
            //{
            //    return null!;
            //}

            //dogToUpdate.Name = request.UpdatedDog.Name;
            //await _dogRepository.UpdateAsync(dogToUpdate);

            //return dogToUpdate;

        }
    }
}
