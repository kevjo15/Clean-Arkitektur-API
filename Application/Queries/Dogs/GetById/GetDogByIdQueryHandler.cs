using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        //private readonly RealDatabase _mockDatabase;
        private readonly AppDbContext _appDbContext;

        public GetDogByIdQueryHandler(/*RealDatabase mockDatabase*/ AppDbContext appDbContext)
        {
            //_mockDatabase = mockDatabase;
            _appDbContext = appDbContext;
        }

        public Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog wantedDog = _appDbContext.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

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
