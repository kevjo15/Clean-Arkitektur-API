using Domain.Models;
using Domain.Models.Animal;
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

        public async Task<UserAnimal> AddUserAnimalAsync(Guid userId, Guid animalId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            var animal = await _appDbContext.Set<AnimalModel>().FindAsync(animalId);

            if (user == null || animal == null)
            {
                throw new ArgumentException("User or Animal not found");
            }

            var userAnimal = new UserAnimal { UserId = userId, AnimalModelId = animalId };
            _appDbContext.UserAnimals.Add(userAnimal);
            await _appDbContext.SaveChangesAsync();

            return userAnimal;
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
            try
            {
                var existingRelation = await _appDbContext.UserAnimals
                    .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimalModelId == currentAnimalModelId);

                if (existingRelation != null)
                {
                    _appDbContext.UserAnimals.Remove(existingRelation);
                }

                var newRelation = new UserAnimal { UserId = userId, AnimalModelId = newAnimalModelId };
                _appDbContext.UserAnimals.Add(newRelation);

                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Kasta ett anpassat undantag eller logga felet
                throw new InvalidOperationException("Failed to update user-animal relationship.", ex);
            }
        }



    }
}
