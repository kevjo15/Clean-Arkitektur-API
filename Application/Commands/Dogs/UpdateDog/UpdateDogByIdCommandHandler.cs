using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        //private readonly RealDatabase _mockDatabase;
        private readonly AppDbContext _appDbContext;

        public UpdateDogByIdCommandHandler(/*RealDatabase mockDatabase*/AppDbContext appDbContext)
        {
            //_mockDatabase = mockDatabase;
            _appDbContext = appDbContext;
        }
        public Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            Dog dogToUpdate = _appDbContext.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            dogToUpdate.Name = request.UpdatedDog.Name;

            return Task.FromResult(dogToUpdate);
        }
    }
}
