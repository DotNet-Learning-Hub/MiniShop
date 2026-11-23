using MiniShop.Domain.Entities;

namespace MiniShop.Data;

public static class StoreData
{
    private static readonly List<Product> Products;
    private static readonly List<User> Users;
    private static readonly List<Order> Orders;

    static StoreData()
    {
        Console.WriteLine("[StoreData] Static constructor called ONE time!");
        Products = SeedData.CreateProducts();
        Users = SeedData.CreateUsers();
        Orders = new List<Order>();
    }

    public static List<Product> GetProducts()
    {
        return Products;
    }

    public static List<User> GetUsers()
    {
        return Users;
    }

    public static List<Order> GetOrders()
    {
        return Orders;
    }

    public static void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public static void AddUser(User user)
    {
        Users.Add(user);
    }

    public static void AddOrder(Order order)
    {
        Orders.Add(order);
    }

    public static int NextProductId()
    {
        return Products.Count + 1;
    }

    public static int NextUserId()
    {
        return Users.Count + 1;
    }

    public static int NextOrderId()
    {
        return Orders.Count + 1;
    }
}
