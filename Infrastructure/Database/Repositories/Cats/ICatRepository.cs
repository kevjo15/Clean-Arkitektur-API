using Domain.Models;

namespace Infrastructure.Database.Repositories.Cats
{
    public interface ICatRepository
    {
        Task<Cat> GetByIdAsync(Guid catId);
        Task<Cat> AddAsync(Cat catToCreate);
        Task UpdateAsync(Cat cat);
        Task DeleteAsync(Guid catId);
        Task<List<Cat>> GetAllCatsAsync();
        Task<IEnumerable<Cat>> GetByBreedAndWeightAsync(string breed, int? weight);
    }
}