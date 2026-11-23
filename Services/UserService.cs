using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class UserService : IUserService
{
    public User FindByUsername(string username)
    {
        foreach (User user in StoreData.GetUsers())
        {
            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                return user;
        }

        return null;
    }

    public User Register(string username, string fullName, string password)
    {
        if (username.Length < 3 || fullName.Length == 0 || password.Length < 4)
            return null;

        if (FindByUsername(username) != null)
            return null;

        User user = new User(StoreData.NextUserId(), username, fullName,
            password, UserRole.Customer);

        StoreData.AddUser(user);
        return user;
    }

    public List<User> GetAllUsers(User admin)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return new List<User>();

        return StoreData.GetUsers();
    }


}
