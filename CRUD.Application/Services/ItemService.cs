using AutoMapper;
using CRUD.Application.Interfaces;
using CRUD.Domain.DTOs;
using CRUD.Domain.Entities;

namespace CRUD.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<List<ItemDto>>(items);
        }

        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return _mapper.Map<ItemDto?>(item);
        }

        public async Task AddAsync(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            await _repository.AddAsync(item);
        }

        public async Task UpdateAsync(ItemDto itemDto)
        {
            if (itemDto.Id <= 0){throw new ArgumentException("O ID do item deve ser maior que zero.");}

            var existingItem = await _repository.GetByIdAsync(itemDto.Id);
            if (existingItem == null){throw new KeyNotFoundException("Item não encontrado para atualização.");}

            var item = _mapper.Map<Item>(itemDto);
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}