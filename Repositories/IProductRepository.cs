using EventDaysMvc.Models;

namespace EventDaysMvc.Repositories;

public interface IProductRepository
{
    IReadOnlyList<Product> GetAll();
    Product? GetById(int id);
    Product Create(string name, decimal price);
    bool Update(int id, string name, decimal price);
    bool Delete(int id);
}
