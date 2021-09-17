using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ItemsController : ControllerBase
  {
    private readonly IItemsRepository _repository;

    public ItemsController(IItemsRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
      return (await _repository.GetItemsAsync()).Select(item => item.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
      var item = await _repository.GetItemAsync(id);

      if (item is null)
      {
        return NotFound();
      }

      return item.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      await _repository.CreateItemAsync(item);

      return CreatedAtAction("GetItem", new { id = item.Id }, item.AsDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = await _repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      var updatedItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      await _repository.UpdateItemAsync(updatedItem);

      return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
      var existingItem = await _repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      await _repository.DeleteItemAsync(id);

      return NoContent();
    }
  }
}