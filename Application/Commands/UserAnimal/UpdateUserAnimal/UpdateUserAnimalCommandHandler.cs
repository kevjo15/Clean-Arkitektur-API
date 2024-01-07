using Infrastructure.Database.Repositories.UserAnimalRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimal.UpdateUserAnimal
{
    public class UpdateUserAnimalCommandHandler : IRequestHandler<UpdateUserAnimalCommand, bool>
    {
        private readonly IUserAnimalRepository _repository;
        private readonly ILogger<UpdateUserAnimalCommandHandler> _logger;

        public UpdateUserAnimalCommandHandler(IUserAnimalRepository repository, ILogger<UpdateUserAnimalCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateUserAnimalCommand request, CancellationToken cancellationToken)
        {


            try
            {
                _logger.LogInformation($"Attempting to update user animal relationship for User ID: {request.UserId}");

                // Implementera logiken för att uppdatera en användar-djur-relation
                await _repository.UpdateUserAnimalAsync(request.UserId, request.CurrentAnimalModelId, request.NewAnimalModelId);
                _logger.LogInformation($"User animal relationship for User ID: {request.UserId} has been successfully updated.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating user animal relationship for User ID: {request.UserId}");
                return false; // Eller hantera eventuella fel på annat sätt
            }


            // Implementera logiken för att uppdatera en användar-djur-relation
            //await _repository.UpdateUserAnimalAsync(request.UserId, request.CurrentAnimalModelId, request.NewAnimalModelId);
            //return true; // Eller hantera eventuella fel och returnera false
        }
    }
}
