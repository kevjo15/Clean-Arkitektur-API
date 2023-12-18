using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<UserModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> RegisterUser(UserModel userToRegister)
        {
            _context.Users.Add(userToRegister);
            _context.SaveChanges();
            return await Task.FromResult(userToRegister);
        }
    }
}
