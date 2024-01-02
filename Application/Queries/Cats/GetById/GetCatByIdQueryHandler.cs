using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Cats.GetById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
    {
        private readonly ICatRepository _catRepository;
        private readonly ILogger<GetCatByIdQueryHandler> _logger;

        public GetCatByIdQueryHandler(ICatRepository catRepository, ILogger<GetCatByIdQueryHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<Cat> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving cat with ID: {request.Id}");

            try
            {
                Cat wantedCat = await _catRepository.GetByIdAsync(request.Id);

                if (wantedCat == null)
                {
                    _logger.LogWarning($"Cat with ID: {request.Id} was not found.");
                    return null; // Eller hantera det på annat sätt beroende på ditt API:s design
                }

                return wantedCat;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving cat with ID: {request.Id}");
                throw; // Kasta om undantaget för att det ska kunna hanteras uppåt i anropskedjan
            }

            //Cat wantedCat = await _catRepository.GetByIdAsync(request.Id);

            //try
            //{
            //    if (wantedCat == null)
            //    {
            //        return null!;
            //    }
            //    return wantedCat;

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
    }
}
