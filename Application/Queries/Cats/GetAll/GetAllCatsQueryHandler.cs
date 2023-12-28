using Domain.Models;
using Infrastructure.Database.Repositories.Cats;
using MediatR;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly ICatRepository _catRepository;
        public GetAllCatsQueryHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            List<Cat> allCatsDatabase = await _catRepository.GetAllCatsAsync();
            if (allCatsDatabase == null)
            {
                throw new InvalidOperationException("No Cats was found!");
            }

            return allCatsDatabase;
        }
    }
}
