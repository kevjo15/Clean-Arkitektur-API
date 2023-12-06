using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommand : IRequest<Bird>
    {
        public UpdateBirdByIdCommand(BirdDto updatedBird, Guid id)
        {
            UpdatedBird = updatedBird;
            Id = id;
        }
        public BirdDto UpdatedBird { get; }
        public Guid Id { get; set; }
    }
}
