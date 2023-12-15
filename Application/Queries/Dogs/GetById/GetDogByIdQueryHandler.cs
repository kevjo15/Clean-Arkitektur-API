using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        //private readonly RealDatabase _mockDatabase;
        //private readonly AppDbContext _appDbContext;
        private readonly IDogRepository _dogRepository;

        public GetDogByIdQueryHandler(/*RealDatabase mockDatabase AppDbContext appDbContext,*/ IDogRepository dogRepository)
        {
            //_mockDatabase = mockDatabase;
            //_appDbContext = appDbContext;
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            //Dog wantedDog = _appDbContext.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            //try
            //{
            //    if (wantedDog == null)
            //    {
            //        return Task.FromResult<Dog>(null!);
            //    }
            //    return Task.FromResult(wantedDog);

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

            Dog wantedDog = await _dogRepository.GetByIdAsync(request.Id);

            try
            {
                if (wantedDog == null)
                {
                    return null!;
                }
                return wantedDog;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
