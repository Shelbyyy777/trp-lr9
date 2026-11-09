using EventDaysMvc.Models;

namespace EventDaysMvc.ViewModels;

public sealed class ProductPageViewModel
{
    public IReadOnlyList<Product> Products { get; set; } = [];
    public WriteProductRequest Input { get; set; } = new();
}

public sealed class EditProductViewModel
{
    public int Id { get; set; }
    public WriteProductRequest Input { get; set; } = new();
}
