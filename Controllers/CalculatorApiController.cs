using EventDaysMvc.Models;
using EventDaysMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventDaysMvc.Controllers;

[ApiController]
[Route("api/days-until")]
public sealed class CalculatorApiController(DayCalculator calculator) : ControllerBase
{
    [HttpGet]
    public ActionResult<DayCalculation> Get([FromQuery] string date) => CalculateDate(date);

    [HttpPost]
    public ActionResult<DayCalculation> Post(CalculateDaysRequest request) => CalculateDate(request.EventDate);

    private ActionResult<DayCalculation> CalculateDate(string? value)
    {
        if (!DayCalculator.TryParseDate(value, out var date))
            return BadRequest(new { code = "INVALID_DATE", message = "Укажите существующую дату в формате yyyy-MM-dd." });
        return Ok(calculator.Calculate(date));
    }
}
