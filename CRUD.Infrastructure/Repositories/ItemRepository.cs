﻿using CRUD.Domain.Entities;
using CRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CRUD.Application.Interfaces;

namespace CRUD.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAllAsync() => await _context.Items.ToListAsync();

        public async Task<Item?> GetByIdAsync(int id) => await _context.Items.FindAsync(id);

        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            if (item.Id <= 0) { throw new ArgumentException("O ID do item deve ser maior que zero.");}

            var existingItem = await _context.Items.FindAsync(item.Id);
            if (existingItem == null) { throw new KeyNotFoundException("Item não encontrado para atualização.");}

            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}