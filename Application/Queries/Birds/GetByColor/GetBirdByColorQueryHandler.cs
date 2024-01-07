using Domain.Models;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Birds.GetByColor
{
    public class GetBirdByColorQueryHandler : IRequestHandler<GetBirdByColorQuery, List<Bird>>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<GetBirdByColorQueryHandler> _logger;

        public GetBirdByColorQueryHandler(IBirdRepository birdRepository, ILogger<GetBirdByColorQueryHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }

        public async Task<List<Bird>> Handle(GetBirdByColorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving birds with color: {request.Color}");

            var birds = await _birdRepository.GetBirdByColorAsync(request.Color);

            if (birds == null || birds.Count == 0)
            {
                _logger.LogWarning($"No birds found with the color: {request.Color}");
                return new List<Bird>(); // Returnera en tom lista istället för null
            }

            _logger.LogInformation($"Retrieved {birds.Count} birds with the color: {request.Color}");
            return birds;

            //return await _birdRepository.GetBirdByColorAsync(request.Color);
        }
    }
}
