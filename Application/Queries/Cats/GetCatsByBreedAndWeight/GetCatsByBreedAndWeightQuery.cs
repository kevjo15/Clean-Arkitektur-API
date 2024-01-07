using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Cats.GetCatsByBreedAndWeight
{
    public class GetCatsByBreedAndWeightQuery : IRequest<IEnumerable<Cat>>
    {
        public string Breed { get; }
        public int? Weight { get; }

        public GetCatsByBreedAndWeightQuery(string breed, int? weight)
        {
            Breed = breed;
            Weight = weight;
        }
    }
}
