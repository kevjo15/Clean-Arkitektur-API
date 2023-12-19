using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, User>
    {
        public readonly IUserRepository _userRepository;
        public DeleteUserByIdCommandHandler(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            User userToDelete = await _userRepository.GetByIdAsync(request.Id);

            if (userToDelete == null)
            {
                throw new InvalidOperationException("No user with the given ID was found.");
            }

            await _userRepository.DeleteAsync(request.Id);

            return (userToDelete);
        }
    }
}
