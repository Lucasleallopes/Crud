using CRUD.Application.Services;
using CRUD.Domain.DTOs;
using CRUD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _service;

        public ItemsController(ItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound("Item não encontrado.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ItemDto item)
        {
            await _service.AddAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ItemDto item)
        {
            if (id != item.Id) {return BadRequest("IDs não coincidem.");}
            if (id <= 0) {return BadRequest("O ID deve ser maior que zero.");}

            var existingItem = await _service.GetByIdAsync(id);
            if (existingItem == null) {return NotFound("Item não encontrado para atualização.");}

            await _service.UpdateAsync(item);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}