using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Repository.Contracts;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAll()
    {
        return _productRepository.GetAll();
    }

    public Product? FindById(int id)
    {
        return _productRepository.FindById(id);
    }

    public Product? AddPhysical(User? admin, string name, int price, int shipping, int stock)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return null;

        if (name.Length == 0 || price < 0 || shipping < 0 || stock < 0)
            return null;

        Product product = new PhysicalProduct(_productRepository.GetNextId(),
            name, price, shipping, stock);
        _productRepository.Add(product);
        return product;
    }

    public Product? AddDigital(User? admin, string name, int price, int discount)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return null;

        if (name.Length == 0 || price < 0 || discount < 0 || discount > 100)
            return null;

        Product product = new DigitalProduct(_productRepository.GetNextId(),
            name, price, discount);
        _productRepository.Add(product);
        return product;
    }

    public bool Deactivate(User? admin, int id)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return false;

        Product? product = FindById(id);
        if (product == null)
            return false;

        product.Deactivate();
        return _productRepository.Update(product);
    }
}
