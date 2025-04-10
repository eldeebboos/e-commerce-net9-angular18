using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        return Ok(await repo.GetProductsAsync(brand, type, sort));
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProducts(Product product)
    {
        repo.AddProduct(product);
        if (await repo.SaveChangesAsync())
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        return BadRequest("Cannot add product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProducts(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        repo.UpdateProduct(product);
        if (await repo.SaveChangesAsync())
            return NoContent();
        return BadRequest("Cannot update product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProducts(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        repo.DeleteProduct(product);
        if (await repo.SaveChangesAsync())
            return NoContent();
        return BadRequest("Cannot delete product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<string>> GetBrands()
    {
        return Ok(await repo.GetBrandsAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<string>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    }

    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }
}
