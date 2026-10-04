using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/products")]
public class StoreProductsController : ControllerBase
{
    private static readonly List<StoreProduct> Products = new()
    {
        new StoreProduct
        {
            Id = 1,
            Name = "Notebook",
            Description = "Учебная тетрадь",
            Price = 350000,
            Quantity = 5
        },
        new StoreProduct
        {
            Id = 2,
            Name = "Mouse",
            Description = "Беспроводная мышь",
            Price = 8000,
            Quantity = 15
        },
        new StoreProduct
        {
            Id = 3,
            Name = "Keyboard",
            Description = "Механическая клавиатура",
            Price = 15000,
            Quantity = 10
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<StoreProduct>> GetAll()
    {
        return Ok(Products);
    }

    [HttpGet("{id:int}")]
    public ActionResult<StoreProduct> GetById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound(new { message = "Товар не найден." });
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<StoreProduct> Create(StoreProduct product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest(new { message = "Название товара обязательно." });
        }

        if (product.Price < 0 || product.Quantity < 0)
        {
            return BadRequest(
                new { message = "Цена и количество не могут быть отрицательными." });
        }

        var newId = Products.Count == 0
            ? 1
            : Products.Max(p => p.Id) + 1;

        product.Id = newId;

        Products.Add(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }

    [HttpPut("{id:int}")]
    public ActionResult<StoreProduct> Update(
        int id,
        StoreProduct product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.Id == id);

        if (existingProduct == null)
        {
            return NotFound(new { message = "Товар не найден." });
        }

        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest(new { message = "Название товара обязательно." });
        }

        if (product.Price < 0 || product.Quantity < 0)
        {
            return BadRequest(
                new { message = "Цена и количество не могут быть отрицательными." });
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;

        return Ok(existingProduct);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound(new { message = "Товар не найден." });
        }

        Products.Remove(product);

        return NoContent();
    }
}