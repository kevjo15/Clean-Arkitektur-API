using Domain.Models;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly ICatRepository _catRepository;
        private readonly ILogger<GetAllCatsQueryHandler> _logger;
        public GetAllCatsQueryHandler(ICatRepository catRepository, ILogger<GetAllCatsQueryHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Starting to process GetAllCatsQuery.");

            try
            {
                var allCatsDatabase = await _catRepository.GetAllCatsAsync();

                if (allCatsDatabase == null || allCatsDatabase.Count == 0)
                {
                    _logger.LogWarning("No cats found in the database.");
                    return new List<Cat>(); // Returnerar en tom lista om inga katter hittades
                }

                _logger.LogInformation($"Retrieved {allCatsDatabase.Count} cats from the database.");
                return allCatsDatabase;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all cats from the database");
                return new List<Cat>(); // Returnerar en tom lista om ett undantag inträffar
            }


            //_logger.LogInformation("Starting to process GetAllCatsQuery.");

            //var allCatsDatabase = await _catRepository.GetAllCatsAsync();

            //if (allCatsDatabase == null || allCatsDatabase.Count == 0)
            //{
            //    _logger.LogWarning("No cats found in the database.");
            //    return new List<Cat>(); // Returnera en tom lista
            //}

            //_logger.LogInformation($"Retrieved {allCatsDatabase.Count} cats from the database.");
            //return allCatsDatabase;
        }
    }
}
