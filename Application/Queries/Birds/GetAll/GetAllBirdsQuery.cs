using Domain.Models;
using MediatR;

namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQuery : IRequest<List<Bird>>
    {
    }
}
