using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        //private readonly RealDatabase _mockDatabase;
       // private readonly AppDbContext _appDbContext;
        private readonly IDogRepository _dogRepository;

        public GetAllDogsQueryHandler(/*RealDatabase mockDatabase, AppDbContext appDbContext*/ IDogRepository dogRepository)
        {
            //_mockDatabase = mockDatabase;
            //_appDbContext = appDbContext; 
            _dogRepository = dogRepository;
        }
        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            //List<Dog> allDogsFromMockDatabase = _mockDatabase.Dogs;
            //return Task.FromResult(allDogsFromMockDatabase);

            List<Dog> allDogsDatabase = await _dogRepository.GetAllDogsAsync();
            if(allDogsDatabase == null)
            {
                throw new InvalidOperationException("No Dogs was found!");
            }

            return allDogsDatabase;

            //var dogs = await _appDbContext.Dogs.Select(d => new Dog { Id = d.Id, Name = d.Name }).ToListAsync();
            //return dogs;
        }
    }
}
