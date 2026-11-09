using EventDaysMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

[Route("students")]
public sealed class StudentPagesController(IStudentRepository repository) : Controller
{
    [HttpGet]
    public IActionResult Index() => View(repository.GetAll());

    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var student = repository.GetById(id);
        return student is null ? NotFound() : View(student);
    }
}
