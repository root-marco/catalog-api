using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ItemsController : ControllerBase
  {
    private readonly InMemItemsRepository _repository;

    public ItemsController()
    {
      _repository = new InMemItemsRepository();
    }

    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
      var items = _repository.GetItems();
      return items;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Item> GetItem(Guid id)
    {
      var item = _repository.GetItem(id);
      
      if (item is null)
      {
        return NotFound();
      }
      
      return item;
    }
  }
}