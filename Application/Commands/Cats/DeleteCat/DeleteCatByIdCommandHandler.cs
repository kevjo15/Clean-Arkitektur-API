using Domain.Models;
using MediatR;
using System;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public DeleteCatByIdCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = await _catRepository.GetByIdAsync(request.Id);
            if (catToDelete == null)
            {
                throw new InvalidOperationException("No cat with the given ID was found.");
            }

            await _catRepository.DeleteAsync(request.Id);

            return catToDelete;
        }
    }
}
