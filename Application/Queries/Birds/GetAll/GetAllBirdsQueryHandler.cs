using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using Infrastructure.Database.Repositories.Cats;
using MediatR;


namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly IBirdRepository _birdRepository;
        public GetAllBirdsQueryHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }
        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> allBirdsDatabase = await _birdRepository.GetAllBirdAsync();
            if (allBirdsDatabase == null)
            {
                throw new InvalidOperationException("No Bird was found!");
            }

            return allBirdsDatabase;
        }
    }
}
