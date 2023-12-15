using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        //private readonly RealDatabase _mockDatabase;
        //private readonly AppDbContext _appDbContext;
        private readonly IDogRepository _dogRepository;

        public AddDogCommandHandler(/*RealDatabase mockDatabase, AppDbContext appDbContext,*/ IDogRepository _dogRepositor)
        {
            //_mockDatabase = mockDatabase;
            //_appDbContext = appDbContext;
            _dogRepository = _dogRepositor;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            //Dog dogToCreate = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = request.NewDog.Name
            //};

            //_mockDatabase.Dogs.Add(dogToCreate);

            //return Task.FromResult(dogToCreate);
            //////////////////////////////////////

            //Dog dogToCreate = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = request.NewDog.Name
            //};

            //_appDbContext.Dogs.Add(dogToCreate);

            //await _appDbContext.SaveChangesAsync();

            //return dogToCreate;

            ////////////////////////////////////

            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            await _dogRepository.AddAsync(dogToCreate);

            return dogToCreate;
        }
    }
}
