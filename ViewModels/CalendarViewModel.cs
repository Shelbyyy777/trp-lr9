using System.ComponentModel.DataAnnotations;
using EventDaysMvc.Models;

namespace EventDaysMvc.ViewModels;

public sealed class CalendarViewModel
{
    [Required(ErrorMessage = "Выберите дату события.")]
    [Display(Name = "Дата события")]
    public string EventDate { get; set; } = string.Empty;
    public string Today { get; set; } = string.Empty;
    public DayCalculation? Result { get; set; }
}
