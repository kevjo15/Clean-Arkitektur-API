using Domain.Models;
using Domain.Models.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IDogRepository
    {
        Task<Dog> GetByIdAsync(Guid dogId);
        Task<Dog> AddAsync(Dog dogToCreate);
        Task UpdateAsync(Dog dog);
        Task DeleteAsync(Guid dogId);
        Task<List<Dog>> GetAllDogsAsync();
    }
}
