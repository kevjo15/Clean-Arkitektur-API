using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<GetAllBirdsQueryHandler> _logger;

        public GetAllBirdsQueryHandler(IBirdRepository birdRepository, ILogger<GetAllBirdsQueryHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }
        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all birds from the database");

            List<Bird> allBirdsDatabase = await _birdRepository.GetAllBirdAsync();

            if (allBirdsDatabase == null || allBirdsDatabase.Count == 0)
            {
                _logger.LogWarning("No birds found in the database.");
                return new List<Bird>(); // Returnera en tom lista istället för att kasta undantag
            }

            _logger.LogInformation($"Retrieved {allBirdsDatabase.Count} birds from the database.");
            return allBirdsDatabase;

            //List<Bird> allBirdsDatabase = await _birdRepository.GetAllBirdAsync();
            //if (allBirdsDatabase == null)
            //{
            //    throw new InvalidOperationException("No Bird was found!");
            //}

            //return allBirdsDatabase;
        }
    }
}
