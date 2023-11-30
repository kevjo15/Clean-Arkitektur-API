using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommand : IRequest<Cat>
    {
        public UpdateCatByIdCommand(CatDto updatedCat, Guid id, bool likesToPlay)
        {
            UpdatedCat = updatedCat;
            Id = id;
            LikesToPlay = likesToPlay;
        }
        public CatDto UpdatedCat { get; }
        public Guid Id { get; set; }
        public bool? LikesToPlay { get; }
    }
}
