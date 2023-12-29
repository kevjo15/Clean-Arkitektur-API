using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public AddCatCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }
        public Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            Cat CatToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                Breed = request.NewCat.Breed,
                Weight = request.NewCat.Weight
            };
            _catRepository.AddAsync(CatToCreate);

            return Task.FromResult(CatToCreate);
        }
    }
}
