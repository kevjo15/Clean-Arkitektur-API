using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        //private readonly RealDatabase _realDatabase;
        private readonly IDogRepository _dogRepository;

        public DeleteDogByIdCommandHandler(/*RealDatabase realdatabase,*/ IDogRepository dogRepository)
        {
            //_realDatabase = realdatabase;
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            // Hitta hunden att ta bort från databasen
            //var dogToDelete = _realDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id);

            //if (dogToDelete != null)
            //{
            //    _realDatabase.Dogs.Remove(dogToDelete);
            //}
            //else
            //{
            //    // Throw an exception or handle the null case as needed for your application
            //    throw new InvalidOperationException("No dog with the given ID was found.");
            //}

            //return Task.FromResult(dogToDelete);

            Dog dogToDelete = await _dogRepository.GetByIdAsync(request.Id);

            if (dogToDelete == null)
            {
                throw new InvalidOperationException("No dog with the given ID was found.");
            }

            await _dogRepository.DeleteAsync(request.Id);

            return (dogToDelete);

        }


    }
}
