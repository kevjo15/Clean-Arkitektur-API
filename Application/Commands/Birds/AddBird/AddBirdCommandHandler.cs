using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly RealDatabase _mockDatabase;

        public AddBirdCommandHandler(RealDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            Bird BirdToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name
            };
            _mockDatabase.Birds.Add(BirdToCreate);

            return Task.FromResult(BirdToCreate);
        }
    }
}
