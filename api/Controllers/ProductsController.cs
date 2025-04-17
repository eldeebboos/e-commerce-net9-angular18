using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await repo.ListAsync(spec);
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProducts(Product product)
    {
        repo.Add(product);
        if (await repo.SaveAllAsync())
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        return BadRequest("Cannot add product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProducts(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        repo.Update(product);
        if (await repo.SaveAllAsync())
            return NoContent();
        return BadRequest("Cannot update product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProducts(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null) return NotFound();

        repo.Remove(product);
        if (await repo.SaveAllAsync())
            return NoContent();
        return BadRequest("Cannot delete product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<string>> GetBrands()
    {
        var spec = new BrandListSpecification();
        var result = await repo.ListAsync(spec);
        return Ok(result);
    }
    [HttpGet("types")]
    public async Task<ActionResult<string>> GetTypes()
    {
        var spec = new TypeListSpecification();
        var result = await repo.ListAsync(spec);

        return Ok(result);
    }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }
}
