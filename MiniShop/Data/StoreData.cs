using MiniShop.Domain.Entities;

namespace MiniShop.Data;

public static class StoreData
{
    private static Product[] _products;
    private static User[] _users;

    static StoreData()
    {
        Console.WriteLine("static constructor called one time!");
        _products = SeedData.CreateProducts();
        _users = SeedData.CreateUsers();
    }

    public static Product[] GetProducts()
    {
        return _products;
    }

    public static User[] GetUsers()
    {
        return _users;
    }


}