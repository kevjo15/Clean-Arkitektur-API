using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly IDogRepository _dogRepository;
        private readonly ILogger<GetDogByIdQueryHandler> _logger;

        public GetDogByIdQueryHandler(IDogRepository dogRepository, ILogger<GetDogByIdQueryHandler> logger)
        {
            _dogRepository = dogRepository;
            _logger = logger;
        }

        public async Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Retrieving dog with ID: {request.Id}");

            try
            {
                Dog wantedDog = await _dogRepository.GetByIdAsync(request.Id);

                if (wantedDog == null)
                {
                    _logger.LogWarning($"Dog with ID: {request.Id} was not found.");
                    return null; // Eller hantera det på annat sätt beroende på ditt API:s design
                }

                return wantedDog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving dog with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //Dog wantedDog = await _dogRepository.GetByIdAsync(request.Id);

            //try
            //{
            //    if (wantedDog == null)
            //    {
            //        return null!;
            //    }
            //    return wantedDog;

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

        }
    }
}
