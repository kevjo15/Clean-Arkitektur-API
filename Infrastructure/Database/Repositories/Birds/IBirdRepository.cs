using Domain.Models;

namespace Infrastructure.Database.Repositories.Birds
{
    public interface IBirdRepository
    {
        Task<Bird> GetByIdAsync(Guid birdId);
        Task<Bird> AddAsync(Bird birdToCreate);
        Task UpdateAsync(Bird bird);
        Task DeleteAsync(Guid birdId);
        Task<List<Bird>> GetAllBirdAsync();
    }
}