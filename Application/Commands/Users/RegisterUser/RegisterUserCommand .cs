using Application.Dtos;
using Domain.Models.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegisterUserCommand(UserDto newUser) 
        {
            NewUser = newUser;
        }
        public UserDto NewUser { get; }
    }
}
