using CRUD.Models;
using CRUD.Migrations;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repository {
    public class ItemRepository(ApplicationDbContext context) {
        public async Task<List<Item>> GetAllAsync() => await context.Items.ToListAsync();
        
        public async Task<Item?> GetByIdAsync(int id) => await context.Items.FindAsync(id);
        
        public async Task AddAsync(Item item) {
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Item item) {
            context.Items.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var item = await context.Items.FindAsync(id);
            if (item != null) {
                context.Items.Remove(item);
                await context.SaveChangesAsync();
            }
        }
    }
}