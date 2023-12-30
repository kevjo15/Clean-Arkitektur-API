using Domain.Models;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using MediatR.Pipeline;
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

        public GetBirdByColorQueryHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<List<Bird>> Handle(GetBirdByColorQuery request, CancellationToken cancellationToken)
        {
            return await _birdRepository.GetBirdByColorAsync(request.Color);
        }
    }
}
