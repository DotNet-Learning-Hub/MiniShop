using MiniShop.Domain.Entities;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class ReportService : IReportService
{
    private readonly IOrderService _orderService;

    public ReportService(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public long GetTotalSales(User? admin)
    {
        long total = 0;

        List<Order> orders = _orderService.GetAllOrders(admin);
        foreach (Order order in orders)
        {
            if (order.Status == MiniShop.Domain.Enums.OrderStatus.Placed)
                total += order.TotalPrice;
        }

        return total;
    }
}
