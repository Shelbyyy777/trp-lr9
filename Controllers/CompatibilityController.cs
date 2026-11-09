using EventDaysMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

// Маршруты из предыдущих работ теперь обрабатывает MVC-контроллер.
[ApiController]
public sealed class CompatibilityController(DayCalculator calculator, IWebHostEnvironment environment) : ControllerBase
{
    [HttpGet("/hello")]
    public IActionResult Hello() => Content("Hello World!", "text/plain; charset=utf-8");

    [HttpGet("/info")]
    public IActionResult Info() => Content("Студент группы БИСТ-24-ПО-3 Янковой Артем", "text/plain; charset=utf-8");

    [HttpGet("/variant")]
    public IActionResult Variant([FromQuery] long? id, [FromQuery] int? total)
    {
        if (id is null or < 0 || total is null or <= 0)
            return BadRequest(new { error = "ID должен быть неотрицательным, число вариантов — положительным." });
        return Ok(new { variant = id.Value % total.Value + 1 });
    }

    [HttpGet("/days-until")]
    public IActionResult DaysUntil([FromQuery] string date)
    {
        if (!DayCalculator.TryParseDate(date, out var parsed))
            return BadRequest(new { error = "Укажите существующую дату в формате yyyy-MM-dd." });
        return Content(calculator.Calculate(parsed).Message, "text/plain; charset=utf-8");
    }

    [HttpGet("/api/demo/slow")]
    public async Task<IActionResult> Slow(CancellationToken cancellationToken)
    {
        if (!environment.IsDevelopment()) return NotFound();
        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        return Ok(new { message = "Операция завершена." });
    }
}
