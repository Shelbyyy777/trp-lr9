using EventDaysMvc.Models;
using EventDaysMvc.Repositories;
using EventDaysMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

[Route("products")]
public sealed class ProductPagesController(IProductRepository repository) : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new ProductPageViewModel { Products = repository.GetAll() });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind(Prefix = "Input")] WriteProductRequest request)
    {
        if (!ModelState.IsValid)
            return View("Index", new ProductPageViewModel { Products = repository.GetAll(), Input = request });
        repository.Create(request.Name, request.Price);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public IActionResult Edit(int id)
    {
        var product = repository.GetById(id);
        if (product is null) return NotFound();
        return View(new EditProductViewModel
        {
            Id = id, Input = new WriteProductRequest { Name = product.Name, Price = product.Price }
        });
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public IActionResult Save(int id, [Bind(Prefix = "Input")] WriteProductRequest request)
    {
        if (repository.GetById(id) is null) return NotFound();
        if (!ModelState.IsValid) return View("Edit", new EditProductViewModel { Id = id, Input = request });
        if (!repository.Update(id, request.Name, request.Price)) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (!repository.Delete(id)) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
