using CRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Infrastructure.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            if (string.IsNullOrEmpty(connectionString)) {
                throw new InvalidOperationException("Connection string 'ConnectionStrings__DefaultConnection' não encontrada.");
            }

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     var connectionString = "Server=localhost;Database=BancoLucas;User=root;Password=root;";
        //     optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Item>().ToTable("Items");
        }
    }
}