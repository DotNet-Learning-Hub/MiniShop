using MiniShop.Domain.Entities;

namespace MiniShop.Services.Contracts;

public interface IOrderService
{
    Order PlaceOrder(User customer, int productId, int quantity);
    List<Order> GetMyOrders(User customer);
    List<Order> GetAllOrders(User admin);
}
