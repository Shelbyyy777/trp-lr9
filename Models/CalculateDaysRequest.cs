using System.ComponentModel.DataAnnotations;

namespace EventDaysMvc.Models;

public sealed class CalculateDaysRequest
{
    [Required(ErrorMessage = "Выберите дату события.")]
    public string EventDate { get; set; } = string.Empty;
}
