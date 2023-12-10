using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
       // private readonly RealDatabase _mockDatabase;
        private readonly AppDbContext _appDbContext;

        public GetAllDogsQueryHandler(/*RealDatabase mockDatabase,*/ AppDbContext appDbContext)
        {
           // _mockDatabase = mockDatabase;
            _appDbContext = appDbContext; 
        }
        public Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            //List<Dog> allDogsFromMockDatabase = _mockDatabase.Dogs;
            //return Task.FromResult(allDogsFromMockDatabase);

            List<Dog> allDogsDatabase = _appDbContext.Dogs.ToList();
            if(allDogsDatabase == null)
            {
                throw new InvalidOperationException("No Dogs was found!");
            }

            return Task.FromResult(allDogsDatabase);

            //var dogs = await _appDbContext.Dogs.Select(d => new Dog { Id = d.Id, Name = d.Name }).ToListAsync();
            //return dogs;
        }
    }
}
