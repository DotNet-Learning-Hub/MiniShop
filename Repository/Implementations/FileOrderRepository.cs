using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;
using Newtonsoft.Json;

namespace MiniShop.Repository.Implementations;

public sealed class FileOrderRepository : IOrderRepository
{
    private readonly string _path;
    private readonly List<Order> _orders;

    public FileOrderRepository(string path)
    {
        _path = path;
        _orders = LoadOrders(path);
    }

    public List<Order> GetAll()
    {
        return _orders;
    }

    public int GetNextId()
    {
        int largestId = 0;
        foreach (Order order in _orders)
        {
            if (order.Id > largestId)
                largestId = order.Id;
        }

        return largestId + 1;
    }

    public bool Add(Order order)
    {
        _orders.Add(order);
        string json = JsonConvert.SerializeObject(_orders, Formatting.Indented);
        File.WriteAllText(_path, json);
        return true;
    }

    private static List<Order> LoadOrders(string path)
    {
        if (!File.Exists(path))
            return new List<Order>();

        string json = File.ReadAllText(path);
        List<Order>? orders = JsonConvert.DeserializeObject<List<Order>>(json);
        return orders ?? new List<Order>();
    }
}
