using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using MediatR;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;

        public AddBirdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }
        public Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            Bird BirdToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name
            };
            _birdRepository.AddAsync(BirdToCreate);

            return Task.FromResult(BirdToCreate);
        }
    }
}
