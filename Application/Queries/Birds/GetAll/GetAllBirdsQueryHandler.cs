using Domain.Models;
using Infrastructure.Database;
using MediatR;


namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly RealDatabase _mockDatabase;
        public GetAllBirdsQueryHandler(RealDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> AllBirdsFromMockDatabase = _mockDatabase.Birds;
            return Task.FromResult(AllBirdsFromMockDatabase);
        }
    }
}
