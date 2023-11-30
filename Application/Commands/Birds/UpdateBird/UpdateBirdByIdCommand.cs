using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommand : IRequest<Bird>
    {
        public UpdateBirdByIdCommand(BirdDto updatedBird, Guid id, bool canFly)
        {
            UpdatedBird = updatedBird;
            Id = id;
            CanFly = canFly;
        }
        public BirdDto UpdatedBird { get; }
        public Guid Id { get; set; }
        public bool? CanFly { get; set; }
    }
}
