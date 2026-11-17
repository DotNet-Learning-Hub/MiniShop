using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;

namespace MiniShop.Services;

public class ProductService
{
    public Product[] GetProducts()
    {
        return StoreData.GetProducts();
    }

    public Product FindById(int id)
    {
        foreach (var product in StoreData.GetProducts())
        {
            if (product.Id == id)
            {
                return product;
            }
        }

        return null;
    }

    public Product AddPhysical(User user , string name, int price, int shippingCost , int stock)
    {
        if (user == null || user.UserRole != UserRole.admin)
        {
            return null;
        }
        
        if (name.Length == 0 || price<0 || shippingCost <0 ||stock <0)
        {
            return null;
        }

        Product product = new PhysicalProduct(1, name, price, shippingCost, stock);
        StoreData.addProduct(product);
        return product;
    }
}