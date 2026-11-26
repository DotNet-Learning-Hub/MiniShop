using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;

namespace MiniShop.Repository.Implementations;

public class InMemoryProductRepository : IProductRepository
{
    protected readonly List<Product> Products;

    public InMemoryProductRepository()
    {
        Products = SeedData.CreateProducts();
    }

    protected InMemoryProductRepository(List<Product> products)
    {
        Products = products;
    }

    public List<Product> GetAll()
    {
        return Products;
    }

    public Product? FindById(int id)
    {
        foreach (Product product in Products)
        {
            if (product.Id == id)
                return product;
        }

        return null;
    }

    public int GetNextId()
    {
        int largestId = 0;
        foreach (Product product in Products)
        {
            if (product.Id > largestId)
                largestId = product.Id;
        }

        return largestId + 1;
    }

    public bool Add(Product product)
    {
        Products.Add(product);
        return true;
    }

    public bool Update(Product product)
    {
        return true;
    }
}
