using Domain.Models;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
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

        public GetCatsByBreedAndWeightQueryHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<IEnumerable<Cat>> Handle(GetCatsByBreedAndWeightQuery request, CancellationToken cancellationToken)
        {
            var cats = await _catRepository.GetByBreedAndWeightAsync(request.Breed, request.Weight);
            return cats;
        }
    }
}
