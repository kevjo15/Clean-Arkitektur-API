using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{

    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateCatByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat catToUpdate = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.Id)!;

            if (catToUpdate != null)
            {
                catToUpdate.Name = request.UpdatedCat.Name;


                if (request.LikesToPlay.HasValue)
                {
                    catToUpdate.LikesToPlay = request.LikesToPlay.Value;
                }
            }

            return Task.FromResult(catToUpdate)!;
        }
    }

}