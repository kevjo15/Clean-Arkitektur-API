using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Queries.Birds.GetById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<GetBirdByIdQueryHandler> _logger;

        public GetBirdByIdQueryHandler(IBirdRepository birdRepository, ILogger<GetBirdByIdQueryHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }

        public async Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving bird with ID: {request.Id}");

            Bird wantedBird = await _birdRepository.GetByIdAsync(request.Id);

            if (wantedBird == null)
            {
                _logger.LogWarning($"Bird with ID: {request.Id} was not found.");
                return null;
            }

            return wantedBird;

            //Bird wantedBird = await _birdRepository.GetByIdAsync(request.Id);

            //try
            //{
            //    if (wantedBird == null)
            //    {
            //        return null!;
            //    }
            //    return wantedBird;

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
    }

}
