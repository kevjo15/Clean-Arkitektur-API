using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Infrastructure.Database.Repositories.Cats
{
    public class CatRepository : ICatRepository
    {
        private readonly AppDbContext _context;

        public CatRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Cat> AddAsync(Cat catToCreate)
        {
            _context.Cats.Add(catToCreate);
            _context.SaveChanges();
            return await Task.FromResult(catToCreate);
        }

        public async Task DeleteAsync(Guid animalId)
        {
            var animalToDelete = await _context.Cats.FindAsync(animalId);
            if (animalToDelete != null)
            {
                _context.Cats.Remove(animalToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Cat>> GetAllCatsAsync()
        {
            return await _context.Cats.ToListAsync();
        }

        public async Task<IEnumerable<Cat>> GetByBreedAndWeightAsync(string breed, int? weight)
        {
            var query = _context.Cats.AsQueryable();

            if (!string.IsNullOrEmpty(breed))
            {
                query = query.Where(c => c.Breed == breed);
            }

            if (weight.HasValue)
            {
                query = query.Where(c => c.Weight == weight.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Cat> GetByIdAsync(Guid catId)
        {
            return await _context.Cats.FindAsync(catId);
        }

        public async Task UpdateAsync(Cat cat)
        {
            _context.Cats.Update(cat);
            await _context.SaveChangesAsync();
        }
    }
}


