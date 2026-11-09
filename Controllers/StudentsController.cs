using EventDaysMvc.Models;
using EventDaysMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

[ApiController]
[Route("api/students")]
public sealed class StudentsController(IStudentRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<Student>> GetAll() => Ok(repository.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = repository.GetById(id);
        return student is null ? NotFound() : Ok(student);
    }
}
