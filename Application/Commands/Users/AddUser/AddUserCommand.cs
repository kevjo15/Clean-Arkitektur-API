using Application.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.AddUser
{
    public class AddUserCommand : IRequest<User>
    {
        public AddUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }
        public UserDto NewUser { get; }
    }
}
