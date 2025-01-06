using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Migrations {
    public class ApplicationDbContext : DbContext {
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionString = "Server=localhost;Database=BancoLucas;User=root;Password=root;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Item>().ToTable("Items");
        }
    }
}