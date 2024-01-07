using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public GetUserByIdQuery(Guid userId)
        {
            Id = userId;
        }
        public Guid Id { get; }
    }
}
