using System.Net;
using System.Net.Http.Json;
using CRUD.Domain.DTOs;

namespace CRUD.Tests
{
    public class ItemsControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public ItemsControllerIntegrationTests()
        {
            var baseAddress = new Uri("http://localhost:5000"); // Porta no Docker Compose
            _client = new HttpClient { BaseAddress = baseAddress };
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithItems()
        {
            var response = await _client.GetAsync("/api/Items");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var items = await response.Content.ReadFromJsonAsync<List<ItemDto>>();
            Assert.NotNull(items);
        }

        [Fact]
        public async Task GetById_ReturnsOkWithItem()
        {
            int testId = 2; // não esqueçe que tem que ter o id banco 

            var response = await _client.GetAsync($"/api/Items/{testId}");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var item = await response.Content.ReadFromJsonAsync<ItemDto>();
            Assert.NotNull(item);
            Assert.Equal(testId, item.Id);
        }

        [Fact]
        public async Task Create_AddsNewItemAndReturnsCreated()
        {
            var newItem = new ItemDto { Id = 0, Name = "New Item", Price = 10.99M };

            var response = await _client.PostAsJsonAsync("/api/Items", newItem);
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdItem = await response.Content.ReadFromJsonAsync<ItemDto>();
            Assert.NotNull(createdItem);
            Assert.Equal(newItem.Name, createdItem.Name);
            Assert.Equal(newItem.Price, createdItem.Price);
        }

        [Fact]
        public async Task Update_UpdatesExistingItem()
        {
            var updateItem = new ItemDto { Id = 1, Name = "Updated Item", Price = 20.99M };

            var response = await _client.PutAsJsonAsync($"/api/Items/{updateItem.Id}", updateItem);
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var getResponse = await _client.GetAsync($"/api/Items/{updateItem.Id}");
            var updatedItem = await getResponse.Content.ReadFromJsonAsync<ItemDto>();
            Assert.Equal(updateItem.Name, updatedItem.Name);
            Assert.Equal(updateItem.Price, updatedItem.Price);
        }

        [Fact]
        public async Task Delete_RemovesItem()
        {
            int idToDelete = 2;

            var response = await _client.DeleteAsync($"/api/Items/{idToDelete}");
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var getResponse = await _client.GetAsync($"/api/Items/{idToDelete}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
