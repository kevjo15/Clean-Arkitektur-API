using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using System;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        //private readonly RealDatabase _mockDatabase;
        //private readonly AppDbContext _appDbContext;
        private readonly IDogRepository _dogRepository;

        public UpdateDogByIdCommandHandler(/*RealDatabase mockDatabaseAppDbContext appDbContext*/ IDogRepository dogRepository)
        {
            //_mockDatabase = mockDatabase;
            //_appDbContext = appDbContext;
            _dogRepository = dogRepository;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            //    Dog dogToUpdate = _appDbContext.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            //    dogToUpdate.Name = request.UpdatedDog.Name;

            //    return Task.FromResult(dogToUpdate);

            Dog dogToUpdate = await _dogRepository.GetByIdAsync(request.Id);

            if (dogToUpdate == null)
            {
                return null!;
            }

            dogToUpdate.Name = request.UpdatedDog.Name;
            await _dogRepository.UpdateAsync(dogToUpdate);

            return dogToUpdate;

        }
    }
}
