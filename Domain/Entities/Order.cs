using MiniShop.Domain.Enums;

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

    public void Cancel()
    {
        Status = OrderStatus.Cancelled;
    }
}
