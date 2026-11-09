using System.ComponentModel.DataAnnotations;

namespace EventDaysMvc.Models;

// Идентификатор не входит в запрос: его назначает репозиторий.
public sealed class WriteProductRequest
{
    [Required(ErrorMessage = "Введите название товара.")]
    [StringLength(100, ErrorMessage = "Название должно содержать не более 100 символов.")]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "Цена должна быть от 0,01 до 1 000 000.")]
    public decimal Price { get; set; }
}
