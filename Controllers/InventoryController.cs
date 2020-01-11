using System;
using System.Linq;
using InventoryAllTheThings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InventoryAllTheThings
{
  [ApiController]
  [Route("api/[controller]")]

  public class InventoryController : ControllerBase
  {

    public DatabaseContext db { get; set; }

    public InventoryController(IConfiguration config)
    {
      this.db = new DatabaseContext(config);
    }

    [HttpGet]
    public ActionResult GetAllInventory()
    {
      return Ok(db.InventoryItems.OrderBy(item => item.DateOrdered));
    }

    [HttpGet("id/{id}")]
    public ActionResult GetItemByIdFromInventory(int id)
    {
      var item = db.InventoryItems.FirstOrDefault(it => it.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    [HttpGet("sku/{sku}")]
    public ActionResult GetItemBySKUFromInventory(string sku)
    {
      var item = db.InventoryItems.FirstOrDefault(it => it.SKU == sku);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    [HttpGet]
    [Route("api/InventoryOutOfStock")]
    public ActionResult GetOutOfStockInventory()
    {
      var itemsOutOfStock = db.InventoryItems.Where(it => it.NumberInStock == 0);
      if (itemsOutOfStock == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(itemsOutOfStock);
      }
    }

    [HttpPost]
    public ActionResult AddInventory(Inventory item)
    {
      // item.Id = 0;
      db.InventoryItems.Add(item);
      db.SaveChanges();
      return Ok(item);
    }

    [HttpPut]
    public ActionResult UpdateInventory(Inventory item)
    {
      var prevItem = db.InventoryItems.FirstOrDefault(it => it.Id == item.Id);
      if (prevItem == null)
      {
        return NotFound();
      }
      else
      {
        prevItem.SKU = item.SKU;
        prevItem.Name = item.Name;
        prevItem.Description = item.Description;
        prevItem.NumberInStock = item.NumberInStock;
        prevItem.Price = item.Price;
        prevItem.DateOrdered = item.DateOrdered;
        db.SaveChanges();
        return Ok(prevItem);
      }

    }

    [HttpDelete("{id}")]
    public ActionResult DeleteInventory(int id)
    {
      var item = db.InventoryItems.FirstOrDefault(it => it.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        db.InventoryItems.Remove(item);
        db.SaveChanges();
        return Ok();
      }
    }
  }
}