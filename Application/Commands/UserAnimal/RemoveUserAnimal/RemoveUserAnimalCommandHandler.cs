using Infrastructure.Database.Repositories.UserAnimalRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimal.RemoveUserAnimal
{
    public class RemoveUserAnimalCommandHandler : IRequestHandler<RemoveUserAnimalCommand, bool>
    {
        private readonly IUserAnimalRepository _repository;
        private readonly ILogger<RemoveUserAnimalCommandHandler> _logger;

        public RemoveUserAnimalCommandHandler(IUserAnimalRepository repository, ILogger<RemoveUserAnimalCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<bool> Handle(RemoveUserAnimalCommand request, CancellationToken cancellationToken)
        {

            try
            {
                _logger.LogInformation($"Attempting to remove user animal relationship for User ID: {request.UserId} and Animal Model ID: {request.AnimalModelId}");

                // Implementera logiken för att ta bort en användar-djur-relation
                await _repository.RemoveUserAnimalAsync(request.UserId, request.AnimalModelId);
                _logger.LogInformation($"User animal relationship for User ID: {request.UserId} and Animal Model ID: {request.AnimalModelId} has been successfully removed.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while removing user animal relationship for User ID: {request.UserId} and Animal Model ID: {request.AnimalModelId}");
                return false; // Eller hantera eventuella fel på annat sätt
            }


            //// Implementera logiken för att ta bort en användar-djur-relation
            //await _repository.RemoveUserAnimalAsync(request.UserId, request.AnimalModelId);
            //return true; // Eller hantera eventuella fel och returnera false
        }
    }
}
