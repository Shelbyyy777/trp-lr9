using System.Globalization;
using EventDaysMvc.Models;

namespace EventDaysMvc.Services;

public sealed class DayCalculator
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    // Сохранена бизнес-логика предыдущего проекта: календарные дни и локальная дата сервера.
    public DayCalculation Calculate(DateOnly eventDate)
    {
        var today = Today;
        var days = eventDate.DayNumber - today.DayNumber;
        var status = days > 0 ? "future" : days == 0 ? "today" : "past";
        var message = days switch
        {
            > 0 => $"До события осталось {days} дней.",
            0 => "Событие сегодня.",
            _ => $"Событие прошло {-days} дней назад."
        };
        return new DayCalculation(Format(eventDate), Format(today), days, status, message);
    }

    public static string Format(DateOnly date) => date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    public static bool TryParseDate(string? value, out DateOnly date) =>
        DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out date);
}
