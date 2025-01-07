using CRUD.Application.Interfaces;
using CRUD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository){
            _repository = repository;
        }

        public async Task<List<Item>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Item?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Item item) => await _repository.AddAsync(item);

        public async Task UpdateAsync(Item item) => await _repository.UpdateAsync(item);

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}