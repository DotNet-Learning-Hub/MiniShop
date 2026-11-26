using MiniShop.Domain.Enums;
using Newtonsoft.Json;

namespace MiniShop.Domain.Entities;

public class Order
{
    public int Id { get; }
    public int UserId { get; }
    public int ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public int UnitPrice { get; }
    public long TotalPrice { get { return (long)UnitPrice * Quantity; } }
    public DateTime CreatedAt { get; }
    public OrderStatus Status { get; private set; }

    public Order(int id, User user, Product product, int quantity)
    {
        Id = id;
        UserId = user.Id;
        ProductId = product.Id;
        ProductName = product.Name;
        Quantity = quantity;
        UnitPrice = product.GetFinalPrice();
        CreatedAt = DateTime.Now;
        Status = OrderStatus.Placed;
    }

    [JsonConstructor]
    public Order(int id, int userId, int productId, string productName,
        int quantity, int unitPrice, DateTime createdAt, OrderStatus status)
    {
        Id = id;
        UserId = userId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CreatedAt = createdAt;
        Status = status;
    }

    public void Cancel()
    {
        Status = OrderStatus.Cancelled;
    }
}
