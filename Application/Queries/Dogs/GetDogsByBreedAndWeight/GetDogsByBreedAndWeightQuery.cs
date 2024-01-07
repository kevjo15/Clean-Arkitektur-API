using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dogs.GetDogsByBreedAndWeight
{
    public class GetDogsByBreedAndWeightQuery : IRequest<IEnumerable<Dog>>
    {
        public GetDogsByBreedAndWeightQuery(string breed, int? weight)
        {
            Breed = breed;
            Weight = weight;
        }

        public string Breed { get; set; }
        public int? Weight { get; set; }
    }
}
