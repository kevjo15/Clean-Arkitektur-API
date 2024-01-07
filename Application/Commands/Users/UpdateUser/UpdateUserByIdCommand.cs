using Application.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommand : IRequest<User>
    {
        public UserDto UpdateUserDto { get; }
        public Guid UserId { get; }
        public string NewPassword { get; set; }
        public UpdateUserByIdCommand(UserDto updateUserDto, Guid userId, string newPassword)
        {
            UpdateUserDto = updateUserDto;
            UserId = userId;
            NewPassword = newPassword;
        }
    }
}
