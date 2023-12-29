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
            return await _dogRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);
        }
    }
}
