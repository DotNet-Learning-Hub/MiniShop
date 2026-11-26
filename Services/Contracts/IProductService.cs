using MiniShop.Domain.Entities;

namespace MiniShop.Services.Contracts;

public interface IProductService
{
    List<Product> GetAll();
    Product? FindById(int id);
    Product? AddPhysical(User? admin, string name, int price, int shipping, int stock);
    Product? AddDigital(User? admin, string name, int price, int discount);
    bool Deactivate(User? admin, int id);
}
