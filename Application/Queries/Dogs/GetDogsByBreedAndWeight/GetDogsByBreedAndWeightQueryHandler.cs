using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;
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

        public GetDogsByBreedAndWeightQueryHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<IEnumerable<Dog>> Handle(GetDogsByBreedAndWeightQuery request, CancellationToken cancellationToken)
        {
            //if (request.Weight.HasValue && string.IsNullOrEmpty(request.Breed))
            //{
            //    // Hämta hundar baserat enbart på vikt
            //    return await _dogRepository.GetDogsByWeightAsync(request.Weight.Value);
            //}
            //else
            //{
                // Hämta hundar baserat på både ras och vikt, eller enbart ras
                return await _dogRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);
            //}
        }
    }
}
