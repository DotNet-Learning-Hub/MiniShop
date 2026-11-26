using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Repository.Contracts;
using MiniShop.Repository.Implementations;
using MiniShop.Services;
using MiniShop.Services.Contracts;



IUserRepository userRepository = new FileUserRepository("users.json");
IProductRepository productRepository = new FileProductRepository(
    "physical-products.json",
    "digital-products.json");
IOrderRepository orderRepository = new FileOrderRepository("orders.json");
// Use InMemoryUserRepository, InMemoryProductRepository and
// InMemoryOrderRepository when persistence is not needed.
IUserService userService = new UserService(userRepository);
IAuthService authService = new AuthService(userService, userRepository);
IProductService productService = new ProductService(productRepository);
IOrderService orderService = new OrderService(productRepository, orderRepository);
IReportService reportService = new ReportService(orderService);

Run();

void Run()
{
    while (true)
    {
        int choice = ConsoleKit.Menu("SIMPLE OOP SHOP", "Login", "Register", "Exit");
        switch (choice)
        {
            case 1: Login(); break;
            case 2: Register(); break;
            case 3: return;
        }
    }
}

void Login()
{
    string username = ConsoleKit.ReadText("Username:");
    string password = ConsoleKit.ReadText("Password:");
    User? user = authService.Login(username, password);

    if (user == null)
    {
        ConsoleKit.Error("Wrong username or password.");
        return;
    }

    ConsoleKit.Success("Welcome " + user.FullName);
    if (user.Role == UserRole.Admin) AdminMenu(user);
    else CustomerMenu(user);
}

void Register()
{
    string username = ConsoleKit.ReadText("Username (min 3 characters):");
    string name = ConsoleKit.ReadText("Full name:");
    string password = ConsoleKit.ReadText("Password (min 4 characters):");
    User? user = authService.Register(username, name, password);

    if (user == null) ConsoleKit.Error("Invalid data or username already exists.");
    else ConsoleKit.Success("Customer created with ID: " + user.Id);
}

void CustomerMenu(User customer)
{
    while (true)
    {
        int choice = ConsoleKit.Menu("CUSTOMER: " + customer.FullName,
            "Show products", "Buy product", "My orders", "My profile", "Change password", "Logout");
        switch (choice)
        {
            case 1: ShowProducts(); break;
            case 2: BuyProduct(customer); break;
            case 3: ShowOrders(orderService.GetMyOrders(customer)); break;
            case 4: ShowProfile(customer); break;
            case 5: ChangePassword(customer); break;
            case 6: return;
        }
    }
}

void AdminMenu(User admin)
{
    while (true)
    {
        int choice = ConsoleKit.Menu("ADMIN: " + admin.FullName,
            "Show products", "Add physical product", "Add digital product", "Deactivate product",
            "Show users", "Show all orders", "Sales report", "Change password", "Logout");
        switch (choice)
        {
            case 1: ShowProducts(); break;
            case 2: AddPhysical(admin); break;
            case 3: AddDigital(admin); break;
            case 4: Deactivate(admin); break;
            case 5: ShowUsers(admin); break;
            case 6: ShowOrders(orderService.GetAllOrders(admin)); break;
            case 7: ShowReport(admin); break;
            case 8: ChangePassword(admin); break;
            case 9: return;
        }
    }
}

void ShowProducts()
{
    ConsoleKit.Title("PRODUCTS");
    foreach (Product product in productService.GetAll())
        ConsoleKit.WriteLine(product.GetDetails());
}

void BuyProduct(User customer)
{
    ShowProducts();
    int id = ConsoleKit.ReadInt("Product ID:", 1);
    int quantity = ConsoleKit.ReadInt("Quantity:", 1);
    Order? order = orderService.PlaceOrder(customer, id, quantity);

    if (order == null) ConsoleKit.Error("Order failed: product not found, inactive or out of stock.");
    else ConsoleKit.Success($"Order #{order.Id} | Total: {order.TotalPrice:N0}");
}

void ShowOrders(List<Order> orders)
{
    ConsoleKit.Title("ORDERS");
    if (orders.Count == 0)
    {
        ConsoleKit.Info("No orders found.");
        return;
    }

    foreach (Order order in orders)
        ConsoleKit.WriteLine($"#{order.Id} | {order.ProductName} | Qty: {order.Quantity} | Total: {order.TotalPrice:N0} | {order.Status}");
}

void ShowProfile(User user)
{
    ConsoleKit.Title("MY PROFILE");
    ConsoleKit.WriteLine($"ID: {user.Id}");
    ConsoleKit.WriteLine($"Username: {user.Username}");
    ConsoleKit.WriteLine($"Name: {user.FullName}");
    ConsoleKit.WriteLine($"Role: {user.Role}");
}

void ChangePassword(User user)
{
    string oldPassword = ConsoleKit.ReadText("Old password:");
    string newPassword = ConsoleKit.ReadText("New password (min 4 characters):");
    if (authService.ChangePassword(user, oldPassword, newPassword)) ConsoleKit.Success("Password changed.");
    else ConsoleKit.Error("Old password is wrong or new password is too short.");
}

void AddPhysical(User admin)
{
    string name = ConsoleKit.ReadText("Product name:");
    int price = ConsoleKit.ReadInt("Base price:", 0);
    int shipping = ConsoleKit.ReadInt("Shipping cost:", 0);
    int stock = ConsoleKit.ReadInt("Stock:", 0);
    Product? product = productService.AddPhysical(admin, name, price, shipping, stock);

    if (product == null) ConsoleKit.Error("Could not add product.");
    else ConsoleKit.Success("Created physical product #" + product.Id);
}

void AddDigital(User admin)
{
    string name = ConsoleKit.ReadText("Product name:");
    int price = ConsoleKit.ReadInt("Base price:", 0);
    int discount = ConsoleKit.ReadInt("Discount percent:", 0, 100);
    Product? product = productService.AddDigital(admin, name, price, discount);

    if (product == null) ConsoleKit.Error("Could not add product.");
    else ConsoleKit.Success("Created digital product #" + product.Id);
}

void Deactivate(User admin)
{
    ShowProducts();
    int id = ConsoleKit.ReadInt("Product ID to deactivate:", 1);
    if (productService.Deactivate(admin, id)) ConsoleKit.Success("Product deactivated.");
    else ConsoleKit.Error("Product not found or access denied.");
}

void ShowUsers(User admin)
{
    ConsoleKit.Title("USERS");
    foreach (User user in userService.GetAllUsers(admin))
        ConsoleKit.WriteLine($"#{user.Id} | {user.Username} | {user.FullName} | {user.Role}");
}

void ShowReport(User admin)
{
    ConsoleKit.Title("SALES REPORT");
    ConsoleKit.WriteLine($"Total sales: {reportService.GetTotalSales(admin):N0}");
}

