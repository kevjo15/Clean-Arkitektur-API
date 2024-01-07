using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
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
        public UserRepository(AppDbContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<User> AddAsync(User userToCreate)
        {
            _context.Users.Add(userToCreate);
            _context.SaveChanges();
            return await Task.FromResult(userToCreate);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var userToDelete = await _context.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be null or empty.", nameof(username));
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
