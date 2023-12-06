using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly RealDatabase _realDatabase;

        public DeleteDogByIdCommandHandler(RealDatabase realdatabase)
        {
            _realDatabase = realdatabase;
        }

        public Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            // Hitta hunden att ta bort från databasen
            var dogToDelete = _realDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id);

            if (dogToDelete != null)
            {
                _realDatabase.Dogs.Remove(dogToDelete);
            }
            else
            {
                // Throw an exception or handle the null case as needed for your application
                throw new InvalidOperationException("No bird with the given ID was found.");
            }


            return Task.FromResult(dogToDelete);

        }


    }
}
