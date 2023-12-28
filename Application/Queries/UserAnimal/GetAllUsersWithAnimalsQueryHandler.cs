﻿using Application.Dtos;
using Domain.Models;
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

            var userAnimalDtos = users.Select(user => new UserAnimalDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Dogs = user.UserAnimals
                    .Where(ua => ua.AnimalModel is Dog)
                    .Select(ua => new DogDto { Name = ua.AnimalModel.Name })
                    .ToList(),
                Cats = user.UserAnimals
                    .Where(ua => ua.AnimalModel is Cat)
                    .Select(ua => new CatDto { Name = ua.AnimalModel.Name, LikesToPlay = ((Cat)ua.AnimalModel).LikesToPlay })
                    .ToList(),
                Birds = user.UserAnimals
                    .Where(ua => ua.AnimalModel is Bird)
                    .Select(ua => new BirdDto { Name = ua.AnimalModel.Name, CanFly = ((Bird)ua.AnimalModel).CanFly })
                    .ToList(),
            });

            return userAnimalDtos;
        }
    }
}
