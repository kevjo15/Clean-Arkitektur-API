using Application.Dtos;
using Infrastructure.Database.Repositories.UserAnimalRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserAnimal
{
    public class GetAllUsersWithAnimalsQueryHandler : IRequestHandler<GetAllUsersWithAnimalsQuery, IEnumerable<UserAnimalDto>>
    {
        private readonly IUserAnimalRepository _repository;

        public GetAllUsersWithAnimalsQueryHandler(IUserAnimalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserAnimalDto>> Handle(GetAllUsersWithAnimalsQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUsersWithAnimalsAsync();
            return users.Select(user => new UserAnimalDto
            {
                UserId = user.Id,
                // Mappa övriga fält beroende på dina behov
            });
        }
    }
}
