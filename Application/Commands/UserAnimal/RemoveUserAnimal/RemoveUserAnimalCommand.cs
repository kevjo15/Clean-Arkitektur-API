using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimal.RemoveUserAnimal
{
    public class RemoveUserAnimalCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid AnimalModelId { get; set; }

        public RemoveUserAnimalCommand(Guid userId, Guid animalModelId)
        {
            UserId = userId;
            AnimalModelId = animalModelId;
        }
    }
}
