using EventDaysMvc.Services;
using EventDaysMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

public sealed class HomeController(DayCalculator calculator) : Controller
{
    [HttpGet("/")]
    public IActionResult Index(string? date)
    {
        var model = new CalendarViewModel
        {
            EventDate = date ?? DayCalculator.Format(calculator.Today.AddDays(30)),
            Today = DayCalculator.Format(calculator.Today)
        };
        SetResult(model);
        return View(model);
    }

    [HttpPost("/calculate")]
    [ValidateAntiForgeryToken]
    public IActionResult Calculate([Bind("EventDate")] CalendarViewModel model)
    {
        model.Today = DayCalculator.Format(calculator.Today);
        SetResult(model);
        return View("Index", model);
    }

    private void SetResult(CalendarViewModel model)
    {
        if (!ModelState.IsValid) return;
        if (!DayCalculator.TryParseDate(model.EventDate, out var date))
        {
            ModelState.AddModelError(nameof(model.EventDate), "Укажите существующую дату в формате yyyy-MM-dd.");
            return;
        }
        model.Result = calculator.Calculate(date);
    }
}
