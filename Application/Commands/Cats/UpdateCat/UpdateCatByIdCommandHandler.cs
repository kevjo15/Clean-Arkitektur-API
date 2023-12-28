using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{

    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public UpdateCatByIdCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat catToUpdate = await _catRepository.GetByIdAsync(request.Id);
            if (catToUpdate == null)
            {
                return null!;
            }

            catToUpdate.Name = request.UpdatedCat.Name;
            catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
            await _catRepository.UpdateAsync(catToUpdate);

            return catToUpdate;

        }
    }

}