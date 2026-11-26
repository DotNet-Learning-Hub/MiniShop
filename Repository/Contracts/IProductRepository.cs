using MiniShop.Domain.Entities;

namespace MiniShop.Repository.Contracts;

public interface IProductRepository
{
    List<Product> GetAll();
    Product? FindById(int id);
    int GetNextId();
    bool Add(Product product);
    bool Update(Product product);
}
