using Domain.Models;
using MediatR;
using System;
using Infrastructure.Database;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, Cat>
    {
        private readonly RealDatabase _mockDatabase;

        public DeleteCatByIdCommandHandler(RealDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.Id);

            if (catToDelete != null)
            {
                _mockDatabase.Cats.Remove(catToDelete);
            }
            else
            {
                // Throw an exception or handle the null case as needed for your application
                throw new InvalidOperationException("No cat with the given ID was found.");
            }


            return Task.FromResult(catToDelete);
        }
    }
}
