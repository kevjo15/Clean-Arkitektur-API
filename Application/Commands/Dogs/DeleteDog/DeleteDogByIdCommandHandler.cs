using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;
        private readonly ILogger<DeleteDogByIdCommandHandler> _logger;

        public DeleteDogByIdCommandHandler(IDogRepository dogRepository, ILogger<DeleteDogByIdCommandHandler> logger)
        {
            _dogRepository = dogRepository;
            _logger = logger;
        }

        public async Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to delete dog with ID: {request.Id}");

            Dog dogToDelete = await _dogRepository.GetByIdAsync(request.Id);
            if (dogToDelete == null)
            {
                _logger.LogWarning($"Dog with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                await _dogRepository.DeleteAsync(request.Id);
                _logger.LogInformation($"Dog with ID: {request.Id} has been successfully deleted.");
                return dogToDelete;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting dog with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //Dog dogToDelete = await _dogRepository.GetByIdAsync(request.Id);

            //if (dogToDelete == null)
            //{
            //    throw new InvalidOperationException("No dog with the given ID was found.");
            //}

            //await _dogRepository.DeleteAsync(request.Id);

            //return (dogToDelete);

        }


    }
}
