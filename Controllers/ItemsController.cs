using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ItemsController : ControllerBase
  {
    private readonly IInMemItemsRepository _repository;

    public ItemsController(IInMemItemsRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public IEnumerable<ItemDto> GetItems()
    {
      var items = _repository.GetItems().Select(item => item.AsDto());
      return items;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
      var item = _repository.GetItem(id);

      if (item is null)
      {
        return NotFound();
      }

      return item.AsDto();
    }
  }
}