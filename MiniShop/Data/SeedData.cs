using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;

namespace MiniShop.Data;

public static class SeedData
{

    public static Product[] CreateProducts()
    {
        return new Product[]
        {
            new PhysicalProduct(1, "Keyboard", 800000, 50000, 4),
            new PhysicalProduct(2, "Mouse", 350000, 30000, 8),
            new DigitalProduct(3, "C# Course", 600000, 10)
        };
    }

    public static User[] CreateUsers()
    {
        return new User[]
        {
            new User(1, "admin", "Store Manager", "1234", UserRole.admin),
            new User(2, "ali", "Ali Ahmadi", "1234", UserRole.customer),
            new User(3, "sara", "Sara Moradi", "1234", UserRole.customer)
        };
    }




}