using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;
using Newtonsoft.Json;

namespace MiniShop.Repository.Implementations;

public sealed class FileProductRepository : IProductRepository
{
    private readonly string _physicalProductsPath;
    private readonly string _digitalProductsPath;
    private readonly List<Product> _products;

    public FileProductRepository(string physicalProductsPath, string digitalProductsPath)
    {
        _physicalProductsPath = physicalProductsPath;
        _digitalProductsPath = digitalProductsPath;
        _products = LoadProducts(physicalProductsPath, digitalProductsPath);
    }

    public List<Product> GetAll()
    {
        return _products;
    }

    public Product? FindById(int id)
    {
        foreach (Product product in _products)
        {
            if (product.Id == id)
                return product;
        }

        return null;
    }

    public int GetNextId()
    {
        int largestId = 0;
        foreach (Product product in _products)
        {
            if (product.Id > largestId)
                largestId = product.Id;
        }

        return largestId + 1;
    }

    public bool Add(Product product)
    {
        _products.Add(product);
        return Update(product);
    }

    public bool Update(Product product)
    {
        var physicalProducts = new List<PhysicalProduct>();
        var digitalProducts = new List<DigitalProduct>();

        foreach (Product item in _products)
        {
            if (item is PhysicalProduct physicalProduct)
                physicalProducts.Add(physicalProduct);

            if (item is DigitalProduct digitalProduct)
                digitalProducts.Add(digitalProduct);
        }

        string physicalJson =
            JsonConvert.SerializeObject(physicalProducts, Formatting.Indented);
        string digitalJson =
            JsonConvert.SerializeObject(digitalProducts, Formatting.Indented);

        File.WriteAllText(_physicalProductsPath, physicalJson);
        File.WriteAllText(_digitalProductsPath, digitalJson);
        return true;
    }

    private static List<Product> LoadProducts(
        string physicalProductsPath,
        string digitalProductsPath)
    {
        if (!File.Exists(physicalProductsPath) || !File.Exists(digitalProductsPath))
            return SeedData.CreateProducts();

        string physicalJson = File.ReadAllText(physicalProductsPath);
        string digitalJson = File.ReadAllText(digitalProductsPath);

        List<PhysicalProduct>? physicalProducts =
            JsonConvert.DeserializeObject<List<PhysicalProduct>>(physicalJson);
        List<DigitalProduct>? digitalProducts =
            JsonConvert.DeserializeObject<List<DigitalProduct>>(digitalJson);

        var products = new List<Product>();

        if (physicalProducts != null)
        {
            foreach (PhysicalProduct product in physicalProducts)
                products.Add(product);
        }

        if (digitalProducts != null)
        {
            foreach (DigitalProduct product in digitalProducts)
                products.Add(product);
        }

        return products;
    }
}
