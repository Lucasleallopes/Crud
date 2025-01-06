using CRUD.Migrations;
using CRUD.Repository;
using CRUD.Controllers;

namespace CRUD {
    public static class Program {
        public static async Task Main(string[] args) {
            await using var context = new ApplicationDbContext();
            var repository = new ItemRepository(context);
            var controller = new ItemController(repository);
            await controller.Run();
        }
    }
}