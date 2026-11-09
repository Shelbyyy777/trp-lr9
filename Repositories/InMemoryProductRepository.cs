using EventDaysMvc.Models;

namespace EventDaysMvc.Repositories;

public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly object _sync = new();
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Яблоко", Price = 1.20m },
        new Product { Id = 2, Name = "Банан", Price = 0.80m }
    ];
    private int _nextId = 3;

    public IReadOnlyList<Product> GetAll()
    {
        lock (_sync) return _products.ToArray();
    }

    public Product? GetById(int id)
    {
        lock (_sync) return _products.FirstOrDefault(product => product.Id == id);
    }

    public Product Create(string name, decimal price)
    {
        lock (_sync)
        {
            // Счётчик работает даже после удаления всех товаров; Max() здесь не нужен.
            var product = new Product { Id = _nextId++, Name = name.Trim(), Price = price };
            _products.Add(product);
            return product;
        }
    }

    public bool Update(int id, string name, decimal price)
    {
        lock (_sync)
        {
            var index = _products.FindIndex(product => product.Id == id);
            if (index < 0) return false;
            _products[index] = new Product { Id = id, Name = name.Trim(), Price = price };
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_sync) return _products.RemoveAll(product => product.Id == id) > 0;
    }
}
