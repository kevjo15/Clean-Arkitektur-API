using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly RealDatabase _mockDatabase;

        public GetDogByIdQueryHandler(RealDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog wantedDog = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            try
            {
                if (wantedDog == null)
                {
                    return Task.FromResult<Dog>(null!);
                }
                return Task.FromResult(wantedDog);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
