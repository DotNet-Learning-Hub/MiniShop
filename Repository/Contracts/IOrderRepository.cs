using MiniShop.Domain.Entities;

namespace MiniShop.Repository.Contracts;

public interface IOrderRepository
{
    List<Order> GetAll();
    int GetNextId();
    bool Add(Order order);
}
