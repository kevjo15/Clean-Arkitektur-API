using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AddCatCommandHandler> _logger;

        public AddCatCommandHandler(ICatRepository catRepository, ILogger<AddCatCommandHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }
        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adding a new cat with name: {request.NewCat.Name}");

            Cat catToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                Breed = request.NewCat.Breed,
                Weight = request.NewCat.Weight
            };

            try
            {
                await _catRepository.AddAsync(catToCreate);
                _logger.LogInformation($"New cat added with ID: {catToCreate.Id}");
                return catToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new cat");
                // Hantera felet på lämpligt sätt, t.ex. genom att returnera null eller kasta ett anpassat undantag
                // Beroende på din applikations design och felhanteringsstrategi
                throw;
            }

            //Cat CatToCreate = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = request.NewCat.Name,
            //    Breed = request.NewCat.Breed,
            //    Weight = request.NewCat.Weight
            //};
            //_catRepository.AddAsync(CatToCreate);

            //return Task.FromResult(CatToCreate);
        }
    }
}
