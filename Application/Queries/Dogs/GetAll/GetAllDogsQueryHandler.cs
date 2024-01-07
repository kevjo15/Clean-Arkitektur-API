using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly IDogRepository _dogRepository;
        private readonly ILogger<GetAllDogsQueryHandler> _logger;

        public GetAllDogsQueryHandler(IDogRepository dogRepository, ILogger<GetAllDogsQueryHandler> logger)
        {
            _dogRepository = dogRepository;
            _logger = logger;
        }
        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {

            try
            {
                _logger.LogInformation("Retrieving all dogs from the database");

                var allDogsDatabase = await _dogRepository.GetAllDogsAsync();

                if (allDogsDatabase == null || allDogsDatabase.Count == 0)
                {
                    _logger.LogWarning("No dogs found in the database.");
                    return new List<Dog>(); // Returnerar en tom lista om inga hundar hittades
                }

                _logger.LogInformation($"Retrieved {allDogsDatabase.Count} dogs from the database.");

                return allDogsDatabase;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all dogs from the database");
                return new List<Dog>(); // Returnerar en tom lista om ett undantag inträffar
            }

            //_logger.LogInformation("Retrieving all dogs from the database");

            //List<Dog> allDogsDatabase = await _dogRepository.GetAllDogsAsync();

            //if (allDogsDatabase == null || allDogsDatabase.Count == 0)
            //{
            //    _logger.LogWarning("No dogs found in the database.");
            //    return new List<Dog>(); // Returnera en tom lista istället för att kasta undantag
            //}

            //_logger.LogInformation($"Retrieved {allDogsDatabase.Count} dogs from the database.");

            //return allDogsDatabase;





            //List<Dog> allDogsDatabase = await _dogRepository.GetAllDogsAsync();
            //if(allDogsDatabase == null)
            //{
            //    throw new InvalidOperationException("No Dogs was found!");
            //}

            //return allDogsDatabase;
        }
    }
}
