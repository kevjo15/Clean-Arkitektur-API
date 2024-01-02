using Domain.Models;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Cats.GetCatsByBreedAndWeight
{
    public class GetCatsByBreedAndWeightQueryHandler : IRequestHandler<GetCatsByBreedAndWeightQuery, IEnumerable<Cat>>
    {
        private readonly ICatRepository _catRepository;
        private readonly ILogger<GetCatsByBreedAndWeightQueryHandler> _logger;

        public GetCatsByBreedAndWeightQueryHandler(ICatRepository catRepository, ILogger<GetCatsByBreedAndWeightQueryHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Cat>> Handle(GetCatsByBreedAndWeightQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Searching for cats with breed: {request.Breed} and weight: {request.Weight}");

            var cats = await _catRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);

            if (cats == null || !cats.Any())
            {
                _logger.LogWarning("No cats found matching the criteria.");
                return new List<Cat>(); // Returnerar en tom lista istället för null
            }

            _logger.LogInformation($"{cats.Count()} cats found matching the criteria.");
            return cats;

            //var cats = await _catRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);
            //return cats;
        }
    }
}
