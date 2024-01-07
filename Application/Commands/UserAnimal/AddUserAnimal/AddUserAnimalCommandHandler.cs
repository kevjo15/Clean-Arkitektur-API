using Application.Dtos;
using Infrastructure.Database.Repositories.UserAnimalRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimal.AddUserAnimal
{
    public class AddUserAnimalCommandHandler : IRequestHandler<AddUserAnimalCommand, UserAnimalDto>
    {
        private readonly IUserAnimalRepository _repository;
        private readonly ILogger<AddUserAnimalCommandHandler> _logger;

        public AddUserAnimalCommandHandler(IUserAnimalRepository repository, ILogger<AddUserAnimalCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserAnimalDto> Handle(AddUserAnimalCommand request, CancellationToken cancellationToken)
        {

            try
            {
                _logger.LogInformation($"Attempting to add user animal relationship for User ID: {request.UserId} and Animal Model ID: {request.AnimalModelId}");

                // Implementera logiken för att lägga till en användar-djur-relation
                await _repository.AddUserAnimalAsync(request.UserId, request.AnimalModelId);

                var userAnimalDto = new UserAnimalDto
                {
                    UserId = request.UserId,
                    // Fyll i övriga fält beroende på dina behov
                };

                _logger.LogInformation($"User animal relationship for User ID: {request.UserId} and Animal Model ID: {request.AnimalModelId} has been successfully added.");
                return userAnimalDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user animal relationship for User ID: {request.UserId} and Animal Model ID: {request.AnimalModelId}");
                // Kasta om undantaget eller hantera det på annat sätt
                throw;
            }


            // Här implementerar du logiken för att lägga till en användar-djur-relation
            // och returnera informationen som ett UserAnimalDto-objekt.
            // Exempel:
            //await _repository.AddUserAnimalAsync(request.UserId, request.AnimalModelId);
            //return new UserAnimalDto
            //{
            //    UserId = request.UserId,
            //    //AnimalModelId = request.AnimalModelId,
            //    // Fyll i övriga fält beroende på dina behov
            //};

        }
    }
}