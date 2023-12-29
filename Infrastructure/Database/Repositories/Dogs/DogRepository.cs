using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class DogRepository : IDogRepository
    {
        private readonly AppDbContext _context;

        public DogRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<Dog> AddAsync(Dog dog)
        {
            _context.Dogs.Add(dog);
            _context.SaveChanges();
            //await _context.SaveChangesAsync();
            return await Task.FromResult(dog);
        }

        public async Task DeleteAsync(Guid animalId)
        {
            var animalToDelete = await _context.Dogs.FindAsync(animalId);
            if (animalToDelete != null)
            {
                _context.Dogs.Remove(animalToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Dog dog)
        {
            _context.Dogs.Update(dog);
            await _context.SaveChangesAsync();
        }
        public async Task<Dog> GetByIdAsync(Guid dogId)
        {
            //List<Dog> allDogsFromDatabase = _context.Dogs.ToList();

            //Dog wantedDog = allDogsFromDatabase.FirstOrDefault(dog => dog.Id == dogId)!;

            //return await Task.FromResult(wantedDog);

            return await _context.Dogs.FindAsync(dogId);
        }


        public async Task<List<Dog>> GetAllDogsAsync()
        {
            //List<Dog> allUsersFromDatabase = _context.Dogs.ToList();
            //return await Task.FromResult(allUsersFromDatabase);
            //båda funkar
            return await _context.Dogs.ToListAsync();

        }

        public async Task<IEnumerable<Dog>> GetByBreedAndWeightAsync(string breed, int? weight)
        {

                var query = _context.Dogs.AsQueryable();

                if (!string.IsNullOrEmpty(breed))
                {
                    query = query.Where(c => c.Breed == breed);
                }

                if (weight.HasValue)
                {
                    query = query.Where(c => c.Weight >= weight.Value);
                }

                return await query.ToListAsync();
        }
    }
}