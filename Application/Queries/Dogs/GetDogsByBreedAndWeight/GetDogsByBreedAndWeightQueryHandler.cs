using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dogs.GetDogsByBreedAndWeight
{
    public class GetDogsByBreedAndWeightQueryHandler : IRequestHandler<GetDogsByBreedAndWeightQuery, IEnumerable<Dog>>
    {
        private readonly IDogRepository _dogRepository;
        private readonly ILogger<GetDogsByBreedAndWeightQueryHandler> _logger;

        public GetDogsByBreedAndWeightQueryHandler(IDogRepository dogRepository, ILogger<GetDogsByBreedAndWeightQueryHandler> logger)
        {
            _dogRepository = dogRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Dog>> Handle(GetDogsByBreedAndWeightQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving dogs with breed: {request.Breed} and weight: {request.Weight}");

            var dogs = await _dogRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);

            if (dogs == null || !dogs.Any())
            {
                _logger.LogWarning("No dogs found matching the criteria.");
                return new List<Dog>(); // Returnerar en tom lista istället för null
            }

            _logger.LogInformation($"Retrieved {dogs.Count()} dogs matching the criteria.");
            return dogs;

            //return await _dogRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);
        }
    }
}
