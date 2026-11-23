using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;

namespace MiniShop.Data;

// فقط وظیفه دارد نمونه های اولیه را بسازد.
public static class SeedData
{
    public static List<Product> CreateProducts()
    {
        return new List<Product>
        {
            new PhysicalProduct(1, "Keyboard", 800000, 50000, 4),
            new PhysicalProduct(2, "Mouse", 350000, 30000, 8),
            new DigitalProduct(3, "C# Course", 600000, 10)
        };
    }
    
    public static List<User> CreateUsers()
    {
        return new List<User>
        {
            new User(1, "admin", "Store Manager", "1234", UserRole.Admin),
            new User(2, "ali", "Ali Ahmadi", "1234", UserRole.Customer),
            new User(3, "sara", "Sara Moradi", "1234", UserRole.Customer)
        };
    }
}
