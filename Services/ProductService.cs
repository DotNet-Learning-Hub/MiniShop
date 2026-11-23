using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class ProductService : IProductService
{
    public List<Product> GetAll()
    {
        return StoreData.GetProducts();
    }

    public Product FindById(int id)
    {
        foreach (Product product in StoreData.GetProducts())
        {
            if (product.Id == id)
                return product;
        }

        return null;
    }

    public Product AddPhysical(User admin, string name, int price, int shipping, int stock)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return null;

        if (name.Length == 0 || price < 0 || shipping < 0 || stock < 0)
            return null;

        Product product = new PhysicalProduct(StoreData.NextProductId(),
            name, price, shipping, stock);
        StoreData.AddProduct(product);
        return product;
    }

    public Product AddDigital(User admin, string name, int price, int discount)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return null;

        if (name.Length == 0 || price < 0 || discount < 0 || discount > 100)
            return null;

        Product product = new DigitalProduct(StoreData.NextProductId(),
            name, price, discount);
        StoreData.AddProduct(product);
        return product;
    }

    public bool Deactivate(User admin, int id)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return false;

        Product product = FindById(id);
        if (product == null)
            return false;

        product.Deactivate();
        return true;
    }
}
