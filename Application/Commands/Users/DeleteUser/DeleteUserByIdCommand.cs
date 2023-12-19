using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommand : IRequest<User>
    {
        public DeleteUserByIdCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
