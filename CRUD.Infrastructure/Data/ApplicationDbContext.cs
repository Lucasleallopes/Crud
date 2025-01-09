using CRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Infrastructure.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

                if (string.IsNullOrEmpty(connectionString)) {
                    connectionString = "Server=db,1433;Database=BancoLucas;User=sa;Password=SenhaForte123!;TrustServerCertificate=True;";
                }

                optionsBuilder.UseSqlServer(connectionString, options => {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });
            }
        }
        
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     var connectionString = "Server=localhost;Database=BancoLucas;User=root;Password=root;";
        //     optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        // } antigo swagger sem dokcer mysql
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Item>(entity => {
                entity.ToTable("Items");
                entity.Property(e => e.Price).HasPrecision(18, 2); // Defina a precisão e escala
            });
        }

    }
}