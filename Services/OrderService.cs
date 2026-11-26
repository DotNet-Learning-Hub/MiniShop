using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Repository.Contracts;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class OrderService : IOrderService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    // ثبت سفارش: پیدا کردن محصول + بررسی خرید + ایجاد Order + ذخیره در Data
    public Order? PlaceOrder(User? customer, int productId, int quantity)
    {
        if (customer == null || customer.Role != UserRole.Customer || quantity <= 0)
            return null;

        Product? product = _productRepository.FindById(productId);

        if (product == null || !product.CanBuy(quantity))
            return null;

        Order order = new Order(_orderRepository.GetNextId(), customer, product, quantity);

        if (!product.Buy(quantity))
            return null;

        _productRepository.Update(product);
        _orderRepository.Add(order);
        return order;
    }

    public List<Order> GetMyOrders(User? customer)
    {
        List<Order> result = new List<Order>();

        if (customer == null || customer.Role != UserRole.Customer)
            return result;

        foreach (Order order in _orderRepository.GetAll())
        {
            if (order.UserId == customer.Id)
            {
                result.Add(order);
            }
        }

        return result;
    }

    public List<Order> GetAllOrders(User? admin)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return new List<Order>();

        return _orderRepository.GetAll();
    }
}
