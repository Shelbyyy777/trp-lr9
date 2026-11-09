namespace EventDaysMvc.Models;

public sealed record DayCalculation(
    string EventDate, string Today, int Days, string Status, string Message);
