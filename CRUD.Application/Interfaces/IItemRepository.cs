using CRUD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(int id);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(int id);
    }
}