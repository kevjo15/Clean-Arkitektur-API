using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repositories.UserAnimalRepository
{
    public class UserAnimalRepository : IUserAnimalRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserAnimalRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<UserAnimal> AddUserAnimalAsync(Guid userId, Guid animalModelId)
        {
            var userAnimal = new UserAnimal { UserId = userId, AnimalModelId = animalModelId };
            _appDbContext.UserAnimals.Add(userAnimal);
            await _appDbContext.SaveChangesAsync();

            return userAnimal; // Returnerar det nyss skapade UserAnimal-objektet
        }
        public async Task RemoveUserAnimalAsync(Guid userId, Guid animalId)
        {
            var userAnimal = await _appDbContext.UserAnimals
                                           .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimalModelId == animalId);
            if (userAnimal != null)
            {
                _appDbContext.UserAnimals.Remove(userAnimal);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>> GetAllUsersWithAnimalsAsync()
        {
            return await _appDbContext.Users
                                 .Include(u => u.UserAnimals)
                                 .ThenInclude(ua => ua.AnimalModel)
                                 .ToListAsync();
        }
        public async Task UpdateUserAnimalAsync(Guid userId, Guid currentAnimalModelId, Guid newAnimalModelId)
        {
            // Ta bort den gamla relationen
            var existingRelation = await _appDbContext.UserAnimals
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimalModelId == currentAnimalModelId);

            if (existingRelation != null)
            {
                _appDbContext.UserAnimals.Remove(existingRelation);
            }

            // Lägg till den nya relationen
            var newRelation = new UserAnimal { UserId = userId, AnimalModelId = newAnimalModelId };
            _appDbContext.UserAnimals.Add(newRelation);

            await _appDbContext.SaveChangesAsync();
        }


    }
}
