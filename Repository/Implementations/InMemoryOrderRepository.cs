using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;

namespace MiniShop.Repository.Implementations;

public class InMemoryOrderRepository : IOrderRepository
{
    protected readonly List<Order> Orders;

    public InMemoryOrderRepository()
    {
        Orders = new List<Order>();
    }

    protected InMemoryOrderRepository(List<Order> orders)
    {
        Orders = orders;
    }

    public List<Order> GetAll()
    {
        return Orders;
    }

    public int GetNextId()
    {
        int largestId = 0;
        foreach (Order order in Orders)
        {
            if (order.Id > largestId)
                largestId = order.Id;
        }

        return largestId + 1;
    }

    public bool Add(Order order)
    {
        Orders.Add(order);
        return true;
    }
}
