using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> AddAsync(User userToCreate);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task<User> FindByUsernameAsync(string username);
    }
}
