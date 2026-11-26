using MiniShop.Domain.Entities;

namespace MiniShop.Services.Contracts;

public interface IReportService
{
    long GetTotalSales(User? admin);
}
