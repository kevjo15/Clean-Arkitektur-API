using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimal.AddUserAnimal
{
    public class AddUserAnimalCommand : IRequest<UserAnimalDto>
    {
        public Guid UserId { get; set; }
        public Guid AnimalModelId { get; set; }
    }
}
