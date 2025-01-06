using CRUD.Models;
using CRUD.Repository;

namespace CRUD.Controllers
{
    // Controlador - Lida com as operações do CRUD
    public class ItemController(ItemRepository repository) {
        public async Task Run()
        {
            Console.WriteLine("Bem-vindo ao CRUD de Itens");
            Console.WriteLine("1. Listar todos os itens");
            Console.WriteLine("2. Obter item por ID");
            Console.WriteLine("3. Adicionar item");
            Console.WriteLine("4. Atualizar item");
            Console.WriteLine("5. Deletar item");

            Console.Write("Escolha uma opção: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    var items = await repository.GetAllAsync();
                    items.ForEach(i => Console.WriteLine($"Id: {i.Id}, Name: {i.Name}, Price: {i.Price:F2}"));
                    break;
                case "2":
                    Console.Write("Digite o ID: ");
                    if (int.TryParse(Console.ReadLine(), out var id))
                    {
                        var item = await repository.GetByIdAsync(id);
                        Console.WriteLine(item != null ? $"Id: {item.Id}, Name: {item.Name}, Price: {item.Price:F2}" : "Item não encontrado.");
                    }
                    break;
                case "3":
                    Console.Write("Digite o nome: ");
                    var name = Console.ReadLine();
                    Console.Write("Digite o preço: ");
                    if (decimal.TryParse(Console.ReadLine(), out var price))
                    {
                        await repository.AddAsync(new Item { Name = name!, Price = price });
                        Console.WriteLine("Item adicionado com sucesso.");
                    }
                    break;
                case "4":
                    Console.Write("Digite o ID do item: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        var itemToUpdate = await repository.GetByIdAsync(id);
                        if (itemToUpdate != null)
                        {
                            Console.Write("Digite o novo nome: ");
                            itemToUpdate.Name = Console.ReadLine()!;
                            Console.Write("Digite o novo preço: ");
                            if (decimal.TryParse(Console.ReadLine(), out price))
                            {
                                itemToUpdate.Price = price;
                                await repository.UpdateAsync(itemToUpdate);
                                Console.WriteLine("Item atualizado com sucesso.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Item não encontrado.");
                        }
                    }
                    break;
                case "5":
                    Console.Write("Digite o ID do item a deletar: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        await repository.DeleteAsync(id);
                        Console.WriteLine("Item deletado com sucesso.");
                    }
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
