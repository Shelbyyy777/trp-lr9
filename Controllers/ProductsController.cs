using EventDaysMvc.Models;
using EventDaysMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(IProductRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<Product>> GetAll() => Ok(repository.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = repository.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(WriteProductRequest request)
    {
        var product = repository.Create(request.Name, request.Price);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, WriteProductRequest request) =>
        repository.Update(id, request.Name, request.Price) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) => repository.Delete(id) ? NoContent() : NotFound();
}
