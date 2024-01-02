using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repositories.Birds
{
    public class BirdRepository : IBirdRepository
    {
        private readonly AppDbContext _context;

        public BirdRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bird> AddAsync(Bird birdToCreate)
        {
            _context.Birds.Add(birdToCreate);
            _context.SaveChanges();
            return await Task.FromResult(birdToCreate);
        }

        public async Task DeleteAsync(Guid animalId)
        {
            var animalToDelete = await _context.Birds.FindAsync(animalId);
            if (animalToDelete != null)
            {
                _context.Birds.Remove(animalToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Bird>> GetAllBirdAsync()
        {
            return await _context.Birds.ToListAsync();
        }

        public async Task<List<Bird>> GetBirdByColorAsync(string color)
        {
            return await _context.Birds
            .OfType<Bird>()
            .Where(b => b.Color == color)
            .OrderByDescending(b => b.Name)
            .ToListAsync();
        }

        public async Task<Bird> GetByIdAsync(Guid birdId)
        {
            return await _context.Birds.FindAsync(birdId);
        }

        public async Task UpdateAsync(Bird bird)
        {
            _context.Birds.Update(bird);
            await _context.SaveChangesAsync();
        }
    }
}
